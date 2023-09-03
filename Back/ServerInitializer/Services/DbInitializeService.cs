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
    page_link      text
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
);

";

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

        var sql = @"insert into public.heading_one (title, page_link)
values  ('Кровля', null),
        ('Фасад', null),
        ('Вентиляция', null),
        ('Водосток', null),
        ('Мансардные окна', null),
        ('Чердачные лестницы', null),
        ('Изоляционные материалы', null),
        ('Ограждения', null),
        ('Комплектующие', null),
        ('Благоустройство', null);";

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
values  ('Парапетные крышки', 8, null, null, true),
        ('Комплектующие для забора', 8, null, null, true),
        ('Элементы безопасной кровли', 9, null, null, true),
        ('Софиты', 9, null, null, true),
        ('Битумные материалы', 7, '109691_bitumnye-materialy.png', null, true),
        ('Кровельные люки', 3, '118597_kartochka_krovelnye-lyuki.png', null, true),
        ('Вентиляционные выходы', 3, '118657_vent-vykhody.png', null, true),
        ('Приточная вентиляция', 3, '118599_pritochnaya-ventilyatsiya.png', null, true),
        ('Планки П-образные', 8, '109955_planki-p-obraznye.png', null, true),
        ('Цокольные дефлекторы', 3, '118600_tsokolnye-deflektory.png', null, true),
        ('Дренажные системы', 10, '109679_drenazhnaya-sitema.png', null, true),
        ('Заборы Жалюзи', 8, '109950_zabory-zhalyuzi.png', null, true),
        ('Утеплители', 7, '109698_uteplitel.png', null, true),
        ('Профилированная мембрана', 7, '109697_profilirovannaya-membrana.png', null, true),
        ('Временные ограждения', 8, '109953_vremennye-ograzhdeniya.png', null, true),
        ('Панельные 3D ограждения', 8, '109954_panelnye-3d-ograzhdeniya.png', null, true),
        ('Герметик', 7, '109693_germetik.png', null, true),
        ('Террасная доска', 10, '109677_terrasnaya-doska.png', null, true),
        ('Сайдинг металлический', 2, '99574_sayding.png', null, true),
        ('Проходные элементы', 3, '118601_prokhodnye-elementy.png', null, true),
        ('Герметизирующие ленты', 7, '109692_germetiziruyuschie-lenty.png', null, true),
        ('Металлочерепица', 1, '99561_metallocherepitsa.png', null, true),
        ('Гибкая черепица', 1, '99565_gibkaya-cherepitsa.png', null, true),
        ('Натуральная черепица', 1, '99566_naturalnaya-cherepitsa.png', null, true),
        ('Керамическая черепица', 1, 'keram.png', null, true),
        ('Композитная черепица', 1, 'kompos.jpg', null, true),
        ('Ондулин', 1, 'ondulin.jpg', null, true),
        ('Доборные элементы кровли', 1, '120683_kartochka-doborka.jpg', null, true),
        ('Элементы безопасности кровли', 1, '109681_bezopasnost-krovli.jpg', null, true),
        ('Профнастил', 1, '110461_99576_profnastil.jpg', null, true),
        ('Козырьки', 9, '109686_kozyrki-krovent.png', null, true),
        ('Уплотнители', 3, '118598_uplotniteli.png', null, true),
        ('Модульные ограждения', 8, '109951_modulnye-ograzhdeniya.png', null, true),
        ('Софиты', 2, '99570_sofity.png', null, true),
        ('Сайдинг виниловый', 2, '99574_sayding.png', null, true),
        ('Фасадная плитка', 2, '120350_fasad-fasadnaya-plitka.png', null, true),
        ('Подсистема для фасада', 2, '120352_fasad_podsistema_dlya-fasada.png', null, true),
        ('Кровельные манжеты', 7, '109696_krovelnye-manzhety.png', null, true),
        ('Флюгеры DUCK & DOG', 9, '109688_flyugery-duckdog.png', null, true),
        ('Доборные элементы', 2, '120351_fasad-dobornye-elementy.png', null, true),
        ('Гидро-пароизоляция', 7, '109694_gidro-paroizolyatsiya.png', null, true),
        ('Фасадные панели', 2, '99568_fasadnye-paneli.png', null, true),
        ('Сайдинг фиброцементный', 2, '99574_sayding.png', null, true),
        ('Штакетники', 8, '109957_metallicheskiy-shtaketnik.png', null, true),
        ('Вентиляция кровли', 3, '118603_ventilyatsiya-krovli.png', null, true),
        ('Вентиляторы', 3, '109682_ventilyatsiya-krovli.png', null, true),
        ('Софиты', 1, '99570_sofity.jpg', null, true),
        ('Оклады для мансардных окно', 5, null, null, true),
        ('Окна-люки', 5, null, null, true),
        ('Аварийные выходы', 5, null, null, true),
        ('Карнизы', 5, null, null, true),
        ('Чердачные лестницы', 6, null, null, false),
        ('Водосток', 4, null, null, false),
        ('Забор из профнастила', 8, '109949_zabory-iz-profnastila.png', null, true);";

        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            await command.ExecuteNonQueryAsync();
        }

        await dbConnection.CloseAsync();
    }
}