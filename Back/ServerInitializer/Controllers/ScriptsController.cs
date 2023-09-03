using System.Data;
using System.Text;
using BackServer.Contexts;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services.Interfaces;
using DbEntity;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlDbExtensions.Enums;
using ServerInitializer.Scripts;
using ServerInitializer.Services;
using HeadingOne = Entity.HeadingOne;

namespace ServerInitializer.Controllers;

[ApiController]
[Route("[controller]")]
public class ScriptsController : ControllerBase
{
    private readonly ILogger<ScriptsController> _logger;
    private readonly IDbInitializerService _dbInitializerService;
    private readonly IDbFillingService _dbFillingService;

    public ScriptsController(ILogger<ScriptsController> logger, IDbInitializerService dbInitializerService,
        IDbFillingService dbFillingService)
    {
        _logger = logger;
        _dbInitializerService = dbInitializerService;
        _dbFillingService = dbFillingService;
    }

    [HttpPost("~/CreateDb")]
    public async Task<ObjectResult> CreateDb()
    {
        try
        {
            await _dbInitializerService.CreateDb();
            await _dbInitializerService.AddHeadingsOne();
            await _dbInitializerService.AddHeadingsTwo();
            return StatusCode(200, "");
        }
        catch (Exception e)
        {
            return StatusCode(400, e.Message);
        }
    }


    [HttpPost("~/InsertAll")]
    public async Task<ObjectResult> InsertAll()
    {
        var map = new Dictionary<HeadingOne, ImportProductClass[]>()
        {
            {
                new HeadingOne("Кровля", "https://vk196.ru/catalog/krovlya"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/profnastil", "Профнастил", "Профнастил"),
                    new ImportProductClass("https://vk196.ru/catalog/metallocherepitsa", "Металлочерепица",
                        "Металлочерепица"),
                    new ImportProductClass("https://vk196.ru/catalog/krovlya/gibkaya-cherepitsa", "Гибкая черепица",
                        null),
                    new ImportProductClass("https://vk196.ru/catalog/krovlya/naturalnaya-cherepitsa",
                        "Натуральная черепица", null),
                    new ImportProductClass("https://vk196.ru/catalog/krovlya/keramicheskaya-cherepitsa",
                        "Керамическая черепица",
                        "Модель черепицы"),
                    new ImportProductClass("https://vk196.ru/catalog/krovlya/kompozitnaya-cherepitsa",
                        "Композитная черепица", "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/krovlya/ondulin", "Ондулин", "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/krovlya/elementy-bezopasnosti-krovli",
                        "Элементы безопасности кровли",
                        null),
                    new ImportProductClass("https://vk196.ru/catalog/krovlya/dobornye-elementy",
                        "Доборные элементы кровли", null),
                    new ImportProductClass("https://vk196.ru/catalog/ehlementi-krovli/sofity", "Софиты", null)
                }
            },
            {
                new HeadingOne("Ограждения", "https://vk196.ru/catalog/zabor"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/zabor/modulnye-ograzhdeniya",
                        "Модульные ограждения", null),
                    new ImportProductClass("https://vk196.ru/catalog/zabor/panelnye-ograzhdeniya",
                        "Панельные 3D ограждения", null),
                    new ImportProductClass("https://vk196.ru/catalog/zabor/shtaketnik-metallicheskiy", "Штакетники",
                        "Вид штакетника"),
                    new ImportProductClass("https://vk196.ru/catalog/zabor/zabor-zhalyuzi", "Заборы Жалюзи", null),
                    new ImportProductClass("https://vk196.ru/catalog/zabor/vremennye-ograzhdeniya",
                        "Временные ограждения", null),
                    new ImportProductClass("https://vk196.ru/catalog/zabor/planki-p-obraznye-zabornye",
                        "Планки П-образные", null),
                    new ImportProductClass("https://vk196.ru/catalog/zabor/parapetnye-kryshki", "Парапетные крышки",
                        null),
                    new ImportProductClass("https://vk196.ru/catalog/zabor/komplektuyuschie-dlya-zabora",
                        "Комплектующие для забора", null)
                }
            },
            {
                new HeadingOne("Комплектующие", "https://vk196.ru/catalog/ehlementi-krovli"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/ehlementi-krovli/flyugery-duck--dog",
                        "Флюгеры DUCK & DOG", null),
                    new ImportProductClass("https://vk196.ru/catalog/ehlementi-krovli/kozyrki-krovent-iz-polikarbonata",
                        "Козырьки", null)
                }
            },
            {
                new HeadingOne("Фасад", "https://vk196.ru/catalog/fasad"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/fasadnye-paneli", "Фасадные панели", "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/fasad/fasadnaya-plitka", "Фасадная плитка",
                        "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/fasad/komplektuyuschie", "Доборные элементы",
                        null),
                    new ImportProductClass("https://vk196.ru/catalog/fasad/podsistema-dlya-ventiliruemogo-fasada",
                        "Подсистема для фасада",
                        "Элемент несущего каркаса"),
                    new ImportProductClass("https://vk196.ru/catalog/fasad/sayding/vinilovyy", "Сайдинг виниловый",
                        "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/fasad/sayding/metallosayding",
                        "Сайдинг металлический", "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/fasad/sayding/fibrotsementnyy",
                        "Сайдинг фиброцементный", "Бренд")
                }
            },
            {
                new HeadingOne("Вентиляция", "https://vk196.ru/catalog/ventilacia"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/ventilacia/krovli", "Вентиляция кровли",
                        "Вентиляция кровли"),
                    new ImportProductClass("https://vk196.ru/catalog/ventilacia/ventilyatsionnye-vykhody",
                        "Вентиляционные выходы",
                        "Вентиляционный выход"),
                    new ImportProductClass("https://vk196.ru/catalog/ventilacia/ventilyatory", "Вентиляторы",
                        "Вентиляторы"),
                    new ImportProductClass("https://vk196.ru/catalog/ventilacia/prokhodnye-elementy",
                        "Проходные элементы",
                        "Проходные элементы"),
                    new ImportProductClass("https://vk196.ru/catalog/ventilacia/tsokolnye-deflektory",
                        "Цокольные дефлекторы",
                        "Цокольные дефлекторы"),
                    new ImportProductClass("https://vk196.ru/catalog/ventilacia/pritochnaya-ventilyatsiya",
                        "Приточная вентиляция",
                        "Приточная вентиляция"),
                    new ImportProductClass("https://vk196.ru/catalog/ventilacia/uplotniteli", "Уплотнители",
                        "Уплотнители"),
                }
            },
            {
                new HeadingOne("Водосток", "https://vk196.ru/catalog/vodostochnye-sistemy"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/vodostochnye-sistemy", "Бренд", "Бренд")
                }
            },
            {
                new HeadingOne("Мансардные окна", "https://vk196.ru/catalog/mansardnye-okna"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/mansardnye-okna/aksessuary-dlya-montazha",
                        "Оклады для мансардных окно",
                        "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/mansardnye-okna/okna-lyuki", "Окна-люки", "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/mansardnye-okna/avariynye-vykhody",
                        "Аварийные выходы", "Стеклопакет"),
                    new ImportProductClass("https://vk196.ru/catalog/mansardnye-okna/karniznoe", "Карнизы", null)
                }
            },
            {
                new HeadingOne("Чердачные лестницы", "https://vk196.ru/catalog/cherdachnye-lestnitsy"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/cherdachnye-lestnitsy", "Чердачные лестницы",
                        "Материал")
                }
            },
            {
                new HeadingOne("Изоляционные материалы", "https://vk196.ru/catalog/uteplenie-i-izolyatsiya"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/uteplenie-i-izolyatsiya/uteplitel", "Утеплители",
                        "Назначение"),
                    new ImportProductClass("https://vk196.ru/catalog/uteplenie-i-izolyatsiya/gidroizolyatsiya",
                        "Гидро-пароизоляция",
                        "Тип изоляции"),
                    new ImportProductClass("https://vk196.ru/catalog/uteplenie-i-izolyatsiya/profilirivannie-membrani",
                        "Профилированная мембрана", "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/uteplenie-i-izolyatsiya/master-flash",
                        "Кровельные манжеты", null),
                    new ImportProductClass("https://vk196.ru/catalog/uteplenie-i-izolyatsiya/germetiki", "Герметик",
                        "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/uteplenie-i-izolyatsiya/bitumnye-materialy",
                        "Битумные материалы", null),
                    new ImportProductClass("https://vk196.ru/catalog/uteplenie-i-izolyatsiya/germetiziruyuschie-lenty",
                        "Герметизирующие ленты",
                        "Бренд")
                }
            },
            {
                new HeadingOne("Благоустройство", "https://vk196.ru/catalog/blagoustroystvo"), new[]
                {
                    new ImportProductClass("https://vk196.ru/catalog/blagoustroystvo/terrasnaya-doska",
                        "Террасная доска", "Бренд"),
                    new ImportProductClass("https://vk196.ru/catalog/blagoustroystvo/drenazhnye-sistemy",
                        "Дренажные системы", "Бренд")
                }
            }
        };
        foreach (var (headingOne, value) in map)
        {

            foreach (var data in value)
            {
                await _dbFillingService.InsertProductCategory(data.CatalogLink, headingOne.Title, data.headingTwoTitle,
                    data.headingThreeProperty);
            }
            await _dbFillingService.InsertFilters(headingOne.PageLink, headingOne.Title, Headings.HeadingOne);
        }

        return StatusCode(200, "");
    }


    [HttpPost("~/InsertCategory")]
    public async Task<ObjectResult> InsertCategory(string categoryRef, string headingOneTitle, string headingTwoTitle,
        string? headingThreeProperty)
    {
        await _dbFillingService.InsertProductCategory(categoryRef, headingOneTitle, headingTwoTitle,
            headingThreeProperty);

        return StatusCode(200, "");
    }

    [HttpPost("~/InsertProduct")]
    public async Task<ObjectResult> InsertProduct(string productRef, string headingOneTitle, string headingTwoTitle,
        string? headingThreeProperty)
    {
        await _dbFillingService.InsertProduct(productRef, headingOneTitle, headingTwoTitle,
            headingThreeProperty);

        return StatusCode(200, "");
    }


    private class ImportProductClass
    {
        public string CatalogLink { get; set; }
        public string headingTwoTitle { get; set; }
        public string? headingThreeProperty { get; set; }

        public ImportProductClass(string catalogLink, string headingTwoTitle, string? headingThreeProperty)
        {
            this.CatalogLink = catalogLink;
            this.headingTwoTitle = headingTwoTitle;
            this.headingThreeProperty = headingThreeProperty;
        }
    }
}