using System.Data;
using BackServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ServerInitializer.Services;

public class DbInitializeService : IDbInitializerService
{
    private GsDbContext _context;

    public DbInitializeService(GsDbContext context)
    {
        _context = context;
    }

    public async Task CreateDb()
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var sql = @"create table if not exists heading_one
(
    heading_one_id serial
        primary key,
    title          varchar(255) not null,
    page_link      text,
    image_ref      text
);

create table if not exists heading_two
(
    heading_two_id serial
        primary key,
    title          varchar(255)         not null,
    heading_one_id integer              not null
        references heading_one
            on update cascade on delete restrict,
    image_ref      text,
    page_link      text,
    is_visible     boolean default true not null
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
    heading_one_id      integer not null
        references heading_one
            on update cascade on delete restrict,
    heading_two_id      integer not null
        references heading_two
            on update cascade on delete restrict,
    unit_measurement_id integer not null
        references units_measurement
            on update cascade on delete set default,
    title               text    not null
);

create table if not exists products
(
    product_id        serial
        primary key,
    heading_three_id  integer
                                               references heading_three
                                                   on update cascade on delete set null,
    title             varchar(255)             not null,
    description       text                     not null,
    price             numeric(10, 2) default 0 not null,
    quantity          integer        default 0 not null,
    popularity        integer        default 0,
    available         boolean                  not null,
    page_link         text,
    product_family_id integer        default 1 not null
        references product_family
            on update cascade on delete restrict,
    last_update_time  timestamp      default CURRENT_TIMESTAMP
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
    heading_one_id     integer           not null
        references heading_one,
    property_values_id integer           not null
        references property_values,
    count_products     integer default 0 not null,
    primary key (heading_one_id, property_values_id)
);

create table if not exists heading_two_filters
(
    heading_two_id     integer           not null
        references heading_two,
    property_values_id integer           not null
        references property_values,
    count_products     integer default 0 not null,
    primary key (heading_two_id, property_values_id)
);";

        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            await command.ExecuteNonQueryAsync();
        }

        await dbConnection.CloseAsync();
    }

    public async Task AddHeadingsOne()
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var sql = @"insert into public.heading_one (title, page_link, image_ref)
values  ('Кровля', null, 'crovlz.png'),
        ('Фасад', null, 'facad.png'),
        ('Вентиляция', null, 'ventilation.png'),
        ('Водосток', null, 'drain.png'),
        ('Мансардные окна', null, 'dormer_windows.png'),
        ('Чердачные лестницы', null, 'stairs.png'),
        ('Изоляционные материалы', null, 'materials.png'),
        ('Ограждения', null, 'fences.png'),
        ('Комплектующие', null, 'accessories.png'),
        ('Благоустройство', null, 'landscaping.png');";

        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            await command.ExecuteNonQueryAsync();
        }

        await dbConnection.CloseAsync();
    }

    public async Task AddHeadingsTwo()
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var sql = @"insert into public.heading_two (title, heading_one_id, image_ref, page_link, is_visible)
values  ( 'Временные ограждения', 8, '109953_vremennye-ograzhdeniya.png', 'Временные ограждения', true),
        ( 'Доборные элементы', 2, '120351_fasad-dobornye-elementy.png', 'Доборные элементы', true),
        ( 'Профилированная мембрана', 7, '109697_profilirovannaya-membrana.png', 'Профилированная мембрана', true),
        ( 'Софиты для кровли', 2, '99570_sofity.png', 'Софиты', true),
        ( 'Композитная черепица', 1, 'kompos.jpg', 'Композитная черепица', true),
        ('Приточная вентиляция', 3, '118599_pritochnaya-ventilyatsiya.png', 'Приточная вентиляция', true),
        ( 'Элементы безопасности кровли', 1, '109681_bezopasnost-krovli.jpg', 'Элементы безопасности кровли', true),
        ( 'Дренажные системы', 10, '109679_drenazhnaya-sitema.png', 'Дренажные системы', true),
        ('Кровельные люки', 3, '118597_kartochka_krovelnye-lyuki.png', 'Кровельные люки', true),
        ('Планки П-образные', 8, '109955_planki-p-obraznye.png', 'Планки П-образные', true),
        ( 'Профнастил', 1, '110461_99576_profnastil.jpg', 'Профнастил', true),
        ( 'Вентиляторы', 3, '109682_ventilyatsiya-krovli.png', 'Вентиляторы', true),
        ( 'Металлочерепица', 1, '99561_metallocherepitsa.png', 'Металлочерепица', true),
        ( 'Цокольные дефлекторы', 3, '118600_tsokolnye-deflektory.png', 'Цокольные дефлекторы', true),
        ('Вентиляционные выходы', 3, '118657_vent-vykhody.png', 'Вентиляционные выходы', true),
        ( 'Гидро-пароизоляция', 7, '109694_gidro-paroizolyatsiya.png', 'Гидро-пароизоляция', true),
        ('Софиты', 9, null, 'Софиты', true),
        ( 'Заборы Жалюзи', 8, '109950_zabory-zhalyuzi.png', 'Заборы Жалюзи', true),
        ( 'Подсистема для фасада', 2, '120352_fasad_podsistema_dlya-fasada.png', 'Подсистема для фасада', true),
        ( 'Сайдинг металлический', 2, '99574_sayding.png', 'Сайдинг металлический', true),
        ( 'Карнизы', 5, null, 'Карнизы', true),
        ( 'Фасадная плитка', 2, '120350_fasad-fasadnaya-plitka.png', 'Фасадная плитка', true),
        ('Битумные материалы', 7, '109691_bitumnye-materialy.png', 'Битумные материалы', true),
        ( 'Вентиляция кровли', 3, '118603_ventilyatsiya-krovli.png', 'Вентиляция кровли', true),
        ( 'Кровельные манжеты', 7, '109696_krovelnye-manzhety.png', 'Кровельные манжеты', true),
        ( 'Проходные элементы', 3, '118601_prokhodnye-elementy.png', 'Проходные элементы', true),
        ( 'Гибкая черепица', 1, '99565_gibkaya-cherepitsa.png', 'Гибкая черепица', true),
        ( 'Ондулин', 1, 'ondulin.jpg', 'Ондулин', true),
        ( 'Чердачные лестницы', 6, null, 'Чердачные лестницы', false),
        ( 'Окна-люки', 5, null, 'Окна-люки', true),
        ( 'Утеплители', 7, '109698_uteplitel.png', 'Утеплители', true),
        ( 'Сайдинг виниловый', 2, '99574_sayding.png', 'Сайдинг виниловый', true),
        ( 'Штакетники', 8, '109957_metallicheskiy-shtaketnik.png', 'Штакетники', true),
        ('Элементы безопасной кровли', 9, null, 'Элементы безопасной кровли', true),
        ('Комплектующиу для забора', 8, null, 'Комплектующиу для забора', true),
        ( 'Герметик', 7, '109693_germetik.png', 'Герметик', true),
        ( 'Герметизирующие ленты', 7, '109692_germetiziruyuschie-lenty.png', 'Герметизирующие ленты', true),
        ( 'Флюгеры DUCK & DOG', 9, '109688_flyugery-duckdog.png', 'Флюгеры DUCK & DOG', true),
        ( 'Оклады для мансардных окно', 5, null, 'Оклады для мансардных окно', true),
        ( 'Террасная доска', 10, '109677_terrasnaya-doska.png', 'Террасная доска', true),
        ( 'Керамическая черепица', 1, 'keram.png', 'Керамическая черепица', true),
        ( 'Модульные ограждения', 8, '109951_modulnye-ograzhdeniya.png', 'Модульные ограждения', true),
        ( 'Фасадные панели', 2, '99568_fasadnye-paneli.png', 'Фасадные панели', true),
        ( 'Доборные элементы кровли', 1, '120683_kartochka-doborka.jpg', 'Доборные элементы кровли', true),
        ( 'Софиты для кровли', 1, '99570_sofity.jpg', 'Софиты', true),
        ( 'Панельные 3D ограждения', 8, '109954_panelnye-3d-ograzhdeniya.png', 'Панельные 3D ограждения', true),
        ( 'Козырьки', 9, '109686_kozyrki-krovent.png', 'Козырьки', true),
        ( 'Аварийные выходы', 5, null, 'Аварийные выходы', true),
        ( 'Водосток', 4, null, 'Водосток', false),
        ( 'Сайдинг фиброцементный', 2, '99574_sayding.png', 'Сайдинг фиброцементный', true),
        ( 'Забор из профнастила', 8, '109949_zabory-iz-profnastila.png', 'Профнастил', true),
        ( 'Натуральная черепица', 1, '99566_naturalnaya-cherepitsa.png', 'Натуральная черепица', true),
        ('Парапетные крышки', 8, null, 'Парапетные крышки', true),
        ( 'Уплотнители', 3, '118598_uplotniteli.png', 'Уплотнители', true);";

        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            await command.ExecuteNonQueryAsync();
        }

        await dbConnection.CloseAsync();
    }
}