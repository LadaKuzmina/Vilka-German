using System.Data;
using System.Text;
using BackServer.Contexts;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Scripts;
using BackServer.Services.Interfaces;
using DbEntity;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;

namespace BackServer.Controllers;

[ApiController]
[Route("[controller]")]
public class ScriptsController : ControllerBase
{
    private readonly ILogger<ScriptsController> _logger;
    private readonly IProductService _productService;
    private readonly IPhotoService _photoService;
    private readonly IPropertyService _propertyService;
    private readonly IHeadersService _headingService;
    private readonly GsDbContext _context;
    private readonly DbAutoInsert _dbAutoInsert;

    public ScriptsController(ILogger<ScriptsController> logger, IProductService productService,
        IPhotoService photoService, IPropertyService propertyService, IHeadersService headersService,
        GsDbContext context, DbAutoInsert dbAutoInsert)
    {
        _logger = logger;
        _productService = productService;
        _photoService = photoService;
        _propertyService = propertyService;
        _headingService = headersService;
        _context = context;
        _dbAutoInsert = dbAutoInsert;
    }

    [HttpPost("~/InsertCategory")]
    public async Task<ObjectResult> InsertCategory(string categoryName, string headingOneTitle, string headingTwoTitle,
        string? headingThreeProperty)
    {
        var refs = new List<string>();
        var pageNum = 1;
        while (true)
        {
            var newRefs = await _dbAutoInsert.GetRefs($"{categoryName}/page{pageNum}");
            if (newRefs.Count == 0)
                break;
            refs.AddRange(newRefs);
            pageNum++;
        }

        foreach (var productRef in refs)
        {
            var product = await _dbAutoInsert.GetProduct(new Uri(productRef));
            if (product.Title == null)
                return StatusCode(400, "");

            product.HeadingOne = headingOneTitle;
            product.HeadingTwo = headingTwoTitle;
            product.ProductFamilyTitle = product.Title;

            // product.HeadingThree = null;
            var propert =
                product.Properties.FirstOrDefault(x => x.Title == headingThreeProperty);
            if (propert != null)
            {
                var headingThreeValue = propert.Values.First();
                var headingThree = new Entity.HeadingThree(headingThreeValue, product.HeadingTwo, null, null);
                await _propertyService.AddPropertyValue(propert.Title, propert.Values);
                var addHeadingThreeSuccess = await _headingService.AddHeadingThree(headingThree);
                if (addHeadingThreeSuccess)
                    product.HeadingThree = headingThreeValue;
                else
                    _logger.Log(LogLevel.Warning,
                        $"Не удалось добавить заголовок третьего уровня {headingThree.Title} к продукту {product.Title}");
            }
            else if (headingThreeProperty!=null)
                _logger.Log(LogLevel.Warning,
                    $"Не удалось добавить заголовок третьего уровня {headingThreeProperty} к продукту {product.Title}");


            var unitMeasurement =
                await _context.UnitMeasurements.FirstOrDefaultAsync(x => x.Value == product.UnitMeasurement);
            if (unitMeasurement == null)
            {
                await _context.UnitMeasurements.AddAsync(new UnitMeasurement() {Value = product.UnitMeasurement});
                await _context.SaveChangesAsync();
            }


            var success = await _productService.Add(product);
            if (!success)
                return StatusCode(400, "");


            var priority = true;
            foreach (var imageRef in product.ImageRefs)
            {
                success = await _photoService.AddProductPhoto(product.Title, imageRef, priority);
                if (!success)
                    _logger.Log(LogLevel.Warning,
                        $"Не удалось добавить изображение {imageRef} к продукту {product.Title}");
                priority = false;
            }

            for (var i = 0; i < Math.Min(3, product.Properties.Count); i++)
            {
                product.Properties[i].IsPriority = true;
            }

            foreach (var property in product.Properties)
            {
                var successes =
                    await _propertyService.AddProductProperties(product.Title, product.Properties.ToArray());
                foreach (var suc in successes)
                {
                    if (!suc)
                        _logger.Log(LogLevel.Warning,
                            $"Не удалось добавить свойство {property.Title} со значением {property.Values} к продукту {product.Title}");
                }
            }
        }

        return StatusCode(200, "");
    }


    [HttpPost("~/InsertAll")]
    public async Task<ObjectResult> InsertAll()
    {
        var map = new Dictionary<string, InCl[]>()
        {
            {
                "Комплектующие", new[]
                {
                    new InCl("ehlementi-krovli/flyugery-duck--dog", "Флюгеры DUCK & DOG", null),
                    new InCl("ehlementi-krovli/kozyrki-krovent-iz-polikarbonata", "Козырьки", null)
                }
            },
            {
                "Кровля", new[]
                {
                    new InCl("profnastil", "Профнастил", "Профнастил"),
                    new InCl("metallocherepitsa", "Металлочерепица", "Металлочерепица"),
                    new InCl("krovlya/gibkaya-cherepitsa", "Гибкая черепица", null),
                    new InCl("krovlya/naturalnaya-cherepitsa", "Натуральная черепица", null),
                    new InCl("krovlya/keramicheskaya-cherepitsa", "Керамическая черепица", "Модель черепицы"),
                    new InCl("krovlya/kompozitnaya-cherepitsa", "Композитная черепица", "Бренд"),
                    new InCl("krovlya/ondulin", "Ондулин", "Бренд"),
                    new InCl("krovlya/elementy-bezopasnosti-krovli", "Элементы безопасности кровли", null),
                    new InCl("krovlya/dobornye-elementy", "Доборные элементы кровли", null),
                    new InCl("ehlementi-krovli/sofity", "Софиты", null)
                }
            },
            {
                "Фасад", new[]
                {
                    new InCl("fasadnye-paneli", "Фасадные панели", "Бренд"),
                    new InCl("fasad/fasadnaya-plitka", "Фасадная плитка", "Бренд"),
                    new InCl("fasad/komplektuyuschie", "Доборные элементы", null),
                    new InCl("fasad/podsistema-dlya-ventiliruemogo-fasada", "Подсистема для фасада", "Элемент несущего каркаса"),
                    new InCl("fasad/sayding/vinilovyy", "Сайдинг виниловый", "Бренд"),
                    new InCl("fasad/sayding/metallosayding", "Сайдинг металлический", "Бренд"),
                    new InCl("fasad/sayding/fibrotsementnyy", "Сайдинг фиброцементный", "Бренд")
                }
            },
            {
                "Вентиляция", new[]
                {
                    new InCl("ventilacia/krovli", "Вентиляция кровли", "Вентиляция кровли"),
                    new InCl("ventilacia/ventilyatsionnye-vykhody", "Вентиляционные выходы", "Вентиляционный выход"),
                    new InCl("ventilacia/ventilyatory", "Вентиляторы", "Вентиляторы"),
                    new InCl("ventilacia/prokhodnye-elementy", "Проходные элементы", "Проходные элементы"),
                    new InCl("ventilacia/tsokolnye-deflektory", "Цокольные дефлекторы", "Цокольные дефлекторы"),
                    new InCl("ventilacia/pritochnaya-ventilyatsiya", "Приточная вентиляция", "Приточная вентиляция"),
                    new InCl("ventilacia/uplotniteli", "Уплотнители", "Уплотнители"),

                }
            },
            {
                "Водосток", new[]
                {
                    new InCl("vodostochnye-sistemy", "Бренд", "Бренд")
                }
            },
            {
                "Мансардные окна", new[]
                {
                    new InCl("mansardnye-okna/aksessuary-dlya-montazha", "Оклады для мансардных окно", "Бренд"),
                    new InCl("mansardnye-okna/okna-lyuki", "Окна-люки", "Бренд"),
                    new InCl("mansardnye-okna/avariynye-vykhody", "Аварийные выходы", "Стеклопакет"),
                    new InCl("mansardnye-okna/karniznoe", "Карнизы", null)
                }
            },
            {
                "Чердачные лестницы", new[]
                {
                    new InCl("cherdachnye-lestnitsy", "Чердачные лестницы", "Материал")
                }
            },
            {
                "Изоляционные материалы", new[]
                {
                    new InCl("uteplenie-i-izolyatsiya/uteplitel", "Утеплители", "Назначение"),
                    new InCl("uteplenie-i-izolyatsiya/gidroizolyatsiya", "Гидро-пароизоляция", "Тип изоляции"),
                    new InCl("uteplenie-i-izolyatsiya/profilirivannie-membrani", "Профилированная мембрана", "Бренд"),
                    new InCl("uteplenie-i-izolyatsiya/master-flash", "Кровельные манжеты", null),
                    new InCl("uteplenie-i-izolyatsiya/germetiki", "Герметик", "Бренд"),
                    new InCl("uteplenie-i-izolyatsiya/bitumnye-materialy", "Битумные материалы", null),
                    new InCl("uteplenie-i-izolyatsiya/germetiziruyuschie-lenty", "Герметизирующие ленты", "Бренд")
                }
            },
            {
                "Ограждения", new[]
                {
                    new InCl("zabor/modulnye-ograzhdeniya", "Модульные ограждения", null),
                    new InCl("zabor/panelnye-ograzhdeniya", "Панельные 3D ограждения", null),
                    new InCl("zabor/shtaketnik-metallicheskiy", "Штакетники", "Вид штакетника"),
                    new InCl("zabor/zabor-zhalyuzi", "Заборы Жалюзи", null),
                    new InCl("zabor/vremennye-ograzhdeniya", "Временные ограждения", null),
                    new InCl("zabor/planki-p-obraznye-zabornye", "Планки П-образные", null),
                    new InCl("zabor/parapetnye-kryshki", "Парапетные крышки", null),
                    new InCl("zabor/komplektuyuschie-dlya-zabora", "Комплектующиу для забора", null)
                }
            },
            {
                "Благоустройство", new[]
                {
                    new InCl("blagoustroystvo/terrasnaya-doska", "Террасная доска", "Бренд"),
                    new InCl("blagoustroystvo/drenazhnye-sistemy", "Дренажные системы", "Бренд")
                }
            }
        };
        foreach (var (headingOneTitle, value) in map)
        {
            foreach (var data in value)
            {
                await InsertCategory(data.reff, headingOneTitle, data.headingTwoTitle, data.headingThreeProperty);
            }
        }

        return StatusCode(200, "");
    }

    [HttpPost("~/CreateDb")]
    public async Task<ObjectResult> CreateDb()
    {
        var resultText = new StringBuilder();

        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var sql = @"create table if not exists heading_one
                        (
                            heading_one_id serial
                                primary key,
                            title          varchar(255) not null,
                            page_link      text
                        );

                        create table if not exists heading_two
                        (
                            heading_two_id serial
                                primary key,
                            title          varchar(255) not null,
                            heading_one_id integer      not null
                                references heading_one
                                    on update cascade on delete restrict,
                            image_ref      text,
                            page_link      text
                        );

                        create table if not exists properties
                        (
                            property_id serial
                                primary key,
                            title       text not null
                        );

                        create table if not exists sales
                        (
                            sale_id     serial
                                primary key,
                            title       text              not null,
                            description text              not null,
                            percent     integer           not null,
                            priority    integer default 0 not null,
                            page_link   text,
                            image_ref   text
                        );

                        create index if not exists sale_priority_index
                            on sales (priority);

                        create table if not exists projects
                        (
                            project_id serial
                                primary key,
                            title      text              not null,
                            roof_type  text,
                            page_link  text,
                            priority   integer default 0 not null
                        );

                        create index if not exists project_priority_index
                            on projects (priority);

                        create table if not exists property_values
                        (
                            property_values_id serial
                                primary key,
                            property_id        integer                         not null
                                references properties
                                    on update cascade on delete restrict,
                            property_value     text default 'Не указано'::text not null
                        );

                        create table if not exists heading_three
                        (
                            heading_three_id   serial
                                primary key,
                            heading_two_id     integer not null
                                references heading_two
                                    on update cascade on delete restrict,
                            property_values_id integer not null
                                references property_values
                                    on update cascade on delete restrict,
                            image_ref          text,
                            page_link          text
                        );

                        create table if not exists units_measurement
                        (
                            unit_measurement_id    serial
                                primary key,
                            unit_measurement_value text not null
                        );

                        create table if not exists product_family
                        (
                            product_family_id   serial
                                primary key,
                            heading_one_id      integer                                                                not null
                                references heading_one
                                    on update cascade on delete restrict,
                            heading_two_id      integer                                                                not null
                                references heading_two
                                    on update cascade on delete restrict,
                            unit_measurement_id integer                                                                not null
                                references units_measurement
                                    on update cascade on delete set default,
                            title               text                                                                   not null
                        );

                        create table if not exists products
                        (
                            product_id        serial
                                primary key,
                            heading_three_id  integer
                                                                  references heading_three
                                                                      on update cascade on delete set null,
                            title             varchar(255)        not null,
                            description       text                not null,
                            price             numeric(10, 2)   default 0 not null,
                            quantity          integer   default 0 not null,
                            popularity        integer   default 0,
                            available         boolean             not null,
                            page_link         text,
                            product_family_id integer   default 1 not null
                                references product_family
                                    on update cascade on delete restrict,
                            last_update_time  timestamp default CURRENT_TIMESTAMP
                        );

                        create index if not exists product_alphabet_index
                            on products (title);

                        create index if not exists product_popularity_index
                            on products (popularity);

                        create index if not exists product_price_index
                            on products (price);

                        create table if not exists product_properties
                        (
                            product_id         integer               not null
                                references products
                                    on update cascade on delete restrict,
                            property_values_id integer               not null
                                references property_values
                                    on update cascade on delete restrict,
                            is_priority        boolean default false not null,
                            primary key (property_values_id, product_id)
                        );

                        create table if not exists sale_products
                        (
                            product_id integer not null
                                references products
                                    on update cascade on delete restrict,
                            sale_id    integer not null
                                references sales
                                    on update cascade on delete restrict,
                            primary key (sale_id, product_id)
                        );

                        create table if not exists project_materials
                        (
                            project_id integer not null
                                references projects
                                    on update cascade on delete restrict,
                            product_id integer not null
                                references products
                                    on update cascade on delete restrict,
                            primary key (project_id, product_id)
                        );

                        create table if not exists product_images
                        (
                            product_id integer                                      not null
                                references products,
                            image_ref  text    default 'profnastil_goods.jpg'::text not null,
                            is_primary boolean default false                        not null,
                            primary key (product_id, image_ref)
                        );

                        create table if not exists project_images
                        (
                            project_id integer               not null
                                references projects,
                            image_ref  text                  not null,
                            is_primary boolean default false not null,
                            primary key (project_id, image_ref)
                        );

                        create table if not exists heading_one_filters
                        (
                            heading_one_id integer not null
                                references heading_one,
                            property_id    integer not null
                                references properties,
                            primary key (heading_one_id, property_id)
                        );

                        create table if not exists heading_two_filters
                        (
                            heading_two_id integer not null
                                references heading_two,
                            property_id    integer not null
                                references properties,
                            primary key (heading_two_id, property_id)
                        );";

        await using var commandCreateTable = new NpgsqlCommand(sql, dbConnection);
        {
            var reader = await commandCreateTable.ExecuteNonQueryAsync();
        }

        sql = @"insert into public.heading_one (title, page_link)
values  ('Кровля', null),
        ('Фасад', null),
        ('Вентиляция', null),
        ('Водосток', null),
        ('Мансардные окна', null),
        ('Чердачные лестницы', null),
        ('Изоляционные материалы', null),
        ('Ограждения', null),
        ('Комплектующие', null),
        ('Благоустройство', null);

insert into public.heading_two (title, heading_one_id, image_ref, page_link)
values  ('Парапетные крышки', 8, null, null),
        ('Комплектующиу для забора', 8, null, null),
        ('Элементы безопасной кровли', 9, null, null),
        ('Софиты', 9, null, null),
        ('Битумные материалы', 7, '109691_bitumnye-materialy.png', null),
        ('Кровельные люки', 3, '118597_kartochka_krovelnye-lyuki.png', null),
        ('Вентиляционные выходы', 3, '118657_vent-vykhody.png', null),
        ('Приточная вентиляция', 3, '118599_pritochnaya-ventilyatsiya.png', null),
        ('Планки П-образные', 8, '109955_planki-p-obraznye.png', null),
        ('Цокольные дефлекторы', 3, '118600_tsokolnye-deflektory.png', null),
        ('Дренажные системы', 10, '109679_drenazhnaya-sitema.png', null),
        ('Заборы Жалюзи', 8, '109950_zabory-zhalyuzi.png', null),
        ('Утеплители', 7, '109698_uteplitel.png', null),
        ('Профилированная мембрана', 7, '109697_profilirovannaya-membrana.png', null),
        ('Временные ограждения', 8, '109953_vremennye-ograzhdeniya.png', null),
        ('Панельные 3D ограждения', 8, '109954_panelnye-3d-ograzhdeniya.png', null),
        ('Герметик', 7, '109693_germetik.png', null),
        ('Террасная доска', 10, '109677_terrasnaya-doska.png', null),
        ('Сайдинг металлический', 2, '99574_sayding.png', null),
        ('Проходные элементы', 3, '118601_prokhodnye-elementy.png', null),
        ('Герметизирующие ленты', 7, '109692_germetiziruyuschie-lenty.png', null),
        ('Металлочерепица', 1, '99561_metallocherepitsa.png', null),
        ('Гибкая черепица', 1, '99565_gibkaya-cherepitsa.png', null),
        ('Натуральная черепица', 1, '99566_naturalnaya-cherepitsa.png', null),
        ('Керамическая черепица', 1, 'keram.png', null),
        ('Композитная черепица', 1, 'kompos.jpg', null),
        ('Ондулин', 1, 'ondulin.jpg', null),
        ('Доборные элементы кровли', 1, '120683_kartochka-doborka.jpg', null),
        ('Элементы безопасности кровли', 1, '109681_bezopasnost-krovli.jpg', null),
        ('Профнастил', 1, '110461_99576_profnastil.jpg', null),
        ('Козырьки', 9, '109686_kozyrki-krovent.png', null),
        ('Уплотнители', 3, '118598_uplotniteli.png', null),
        ('Модульные ограждения', 8, '109951_modulnye-ograzhdeniya.png', null),
        ('Софиты', 2, '99570_sofity.png', null),
        ('Сайдинг виниловый', 2, '99574_sayding.png', null),
        ('Фасадная плитка', 2, '120350_fasad-fasadnaya-plitka.png', null),
        ('Подсистема для фасада', 2, '120352_fasad_podsistema_dlya-fasada.png', null),
        ('Кровельные манжеты', 7, '109696_krovelnye-manzhety.png', null),
        ('Флюгеры DUCK & DOG', 9, '109688_flyugery-duckdog.png', null),
        ('Доборные элементы', 2, '120351_fasad-dobornye-elementy.png', null),
        ('Гидро-пароизоляция', 7, '109694_gidro-paroizolyatsiya.png', null),
        ('Фасадные панели', 2, '99568_fasadnye-paneli.png', null),
        ('Сайдинг фиброцементный', 2, '99574_sayding.png', null),
        ('Штакетники', 8, '109957_metallicheskiy-shtaketnik.png', null),
        ('Вентиляция кровли', 3, '118603_ventilyatsiya-krovli.png', null),
        ('Вентиляторы', 3, '109682_ventilyatsiya-krovli.png', null),
        ('Софиты', 1, '99570_sofity.jpg', null),
        ('Оклады для мансардных окно', 5, null, null),
                ('Окна-люки', 5, null, null),
                        ('Аварийные выходы', 5, null, null),
                                ('Карнизы', 5, null, null),
        ('Чердачные лестницы', 6, null, null),
        ('Бренд', 4, null, null),
        ('Профнастил', 8, '109949_zabory-iz-profnastila.png', null);";

        await using var commandInsertDefaultValues = new NpgsqlCommand(sql, dbConnection);
        {
            await commandInsertDefaultValues.ExecuteNonQueryAsync();
        }

        await dbConnection.CloseAsync();

        return StatusCode(200, resultText.ToString());
    }

    private class InCl
    {
        public string reff { get; set; }
        public string headingTwoTitle { get; set; }
        public string? headingThreeProperty { get; set; }

        public InCl(string reff, string headingTwoTitle, string? headingThreeProperty)
        {
            this.reff = reff;
            this.headingTwoTitle = headingTwoTitle;
            this.headingThreeProperty = headingThreeProperty;
        }
    }
}