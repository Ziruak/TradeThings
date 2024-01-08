using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFViewSwitchNavigationDependencyInjection.MVVM.Model;

internal static class Goods
{
    // Рацион
    public static readonly Good Ration = new Good("Ration", 5f, new List<GoodType> { GoodType.Food });
    // Кинжал
    public static readonly Good Dagger = new Good("Dagger", 20f, new List<GoodType> { GoodType.Metal, GoodType.Weapon });
    // Кусок золота
    public static readonly Good GoldPiece = new Good("Gold piece", 500f, new List<GoodType> { GoodType.Metal, GoodType.Valuables });
    // Магическая сфера
    public static readonly Good BMagicalSphere = new Good("Basic magical sphere", 600f, new List<GoodType> { GoodType.Magical });
    // Магическая сфера с драгоценностями
    public static readonly Good EMagicalSphere = new Good("Encrusted magical sphere", 1000f, new List<GoodType> { GoodType.Magical, GoodType.Valuables });
    //Курица
    public static readonly Good Chicken = new Good("Chicken", 0.2f, new List<GoodType> { GoodType.Animal });
    //Железо
    public static readonly Good Iron = new Good("Iron", 1f, new List<GoodType> { GoodType.Metal });
    //Холст
    public static readonly Good Canvas = new Good("Canvas", 1f, new List<GoodType> { GoodType.Utensils });
    //Свинья
    public static readonly Good Pig = new Good("Pig", 30f, new List<GoodType> { GoodType.Animal });
    //Корова
    public static readonly Good Cow = new Good("Cow", 100f, new List<GoodType> { GoodType.Animal });



    //Дневник
    public static readonly Good Diary = new Good("Diary", 5f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });
    //Латунное кольцо
    public static readonly Good BrassRing = new Good("Brass ring", 30f, new List<GoodType> { GoodType.Trinkets, GoodType.Valuables });
    //Рецепт
    public static readonly Good Receipt = new Good("Receipt", 2f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });
    //Перо 
    public static readonly Good Stylus = new Good("Stylus", 0.2f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });
    //Коготь дракона
    public static readonly Good DragonsClaw = new Good("Dragons claw", 1000f, new List<GoodType> { GoodType.Trinkets, GoodType.Valuables, GoodType.Animal });
    //Книга
    public static readonly Good Book = new Good("Book", 250f, new List<GoodType> { GoodType.Trinkets, GoodType.Valuables });
    //Нож
    public static readonly Good Knife = new Good("Knife", 0.5f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });
    //Сундук
    public static readonly Good Chest = new Good("Chest", 50f, new List<GoodType> { GoodType.Trinkets, GoodType.Wooden, GoodType.Utensils });
    //Свеча
    public static readonly Good Candle = new Good("Candle", 0.1f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });
    //Иголка
    public static readonly Good Needle = new Good("Needle", 0.5f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils, GoodType.Metal });
    //Стеклянная бутылка
    public static readonly Good GlassBottle = new Good("Glass bottle", 20f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });

    //Оружие

    //Боевой посох
    public static readonly Good BattleStaff = new Good("Battle staff", 2f, new List<GoodType> { GoodType.Weapon, GoodType.Wooden });
    //Булава
    public static readonly Good Mace = new Good("Mace", 50f, new List<GoodType> { GoodType.Weapon, GoodType.Wooden, GoodType.Metal });
    //ДУбинка
    public static readonly Good Baton = new Good("Baton", 1f, new List<GoodType> { GoodType.Weapon, GoodType.Wooden });
    //Арбалет
    public static readonly Good Crossbow = new Good("Crossbow", 500f, new List<GoodType> { GoodType.Weapon, GoodType.Wooden });
    //Ручной топор
    public static readonly Good HandAxe = new Good("Hand axe", 50f, new List<GoodType> { GoodType.Weapon, GoodType.Wooden, GoodType.Metal});
    //Меч
    public static readonly Good Sword = new Good("Sword", 150f, new List<GoodType> { GoodType.Weapon, GoodType.Metal });

    //Доспехи

    //Стеганый
    public static readonly Good QuiltedArmor = new Good("Quilted armor", 50f, new List<GoodType> { GoodType.Armor });
    //Кожаный
    public static readonly Good LeatherArmor = new Good("Leather armor", 100f, new List<GoodType> { GoodType.Armor, GoodType.Leather });
    //Шкурный
    public static readonly Good HideArmor = new Good("Hide armor", 100f, new List<GoodType> { GoodType.Armor, GoodType.Animal });
    //Чешуйчатый
    public static readonly Good ScalyArmor = new Good("Scaly armor", 500f, new List<GoodType> { GoodType.Armor, GoodType.Metal, GoodType.Leather });
    //Кираса
    public static readonly Good Cuirass = new Good("Cuirass", 4000f, new List<GoodType> { GoodType.Armor, GoodType.Metal, GoodType.Leather });
    //Кольчуга
    public static readonly Good ChainMail = new Good("Chain male", 750f, new List<GoodType> { GoodType.Armor, GoodType.Metal, GoodType.Leather });


    //Табакерка
    public static readonly Good Snuffbox = new Good("Snuffbox", 80f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });
    //Носовой платок
    public static readonly Good Handkerchief = new Good("Handkerchief", 170f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });
    //Ключ
    public static readonly Good Key = new Good("Key", 300f, new List<GoodType> { GoodType.Trinkets, GoodType.Valuables, GoodType.Metal });
    //Камень
    public static readonly Good Stone = new Good("Stone", 10f, new List<GoodType> { GoodType.Trinkets });
    //Шкатулка
    public static readonly Good Casket = new Good("Casket", 500f, new List<GoodType> { GoodType.Trinkets, GoodType.Utensils });

    //Нелегальные медицинские препараты*
    public static readonly Good Drugs = new Good("Drugs", 250f, new List<GoodType> { GoodType.Illegal });
    //Сладости
    public static readonly Good Sweetw = new Good("Sweets", 150f, new List<GoodType> { GoodType.Food });

    public static readonly List<Good> AllGoods = new List<Good> { 
    Ration, Dagger, GoldPiece, BMagicalSphere, EMagicalSphere, Chicken, Iron, Canvas, Pig, Cow, Diary, BrassRing, Receipt, Stylus, DragonsClaw, Book, Knife, Chest, Candle, Needle,
    GlassBottle, BattleStaff, Mace, Baton, Crossbow, HandAxe, QuiltedArmor, LeatherArmor, HideArmor, ScalyArmor, Cuirass,
    ChainMail, Snuffbox, Handkerchief, Key, Stone, Casket
    };
}
