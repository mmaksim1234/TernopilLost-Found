namespace server2MVC.Models
{
    public class CategoryClassifier
    {
        private static readonly List<string> BijouterieKeywords = new List<string>
    {
         "Намисто", "Браслет", "Сережки", "Кільце", "Кулон", "Брошка", "Перли", "Ланцюжок", "Шармик", "Підвіска", "Каблучка", "Комплект",
        "Чокер", "Обручка", "Камінь", "Сапфір", "Смарагд", "Діамант", "Золотий", "Срібний", "Платина", "Кристали", "Фіаніт",
        "Мереживний", "Вишивка", "Метал", "Стрази", "Гранат", "Аметист", "Агат"
    };

        private static readonly List<string> ClothingKeywords = new List<string>
    {
          "Сукня", "Куртка", "Штани", "Футболка", "Светр", "Кофта", "Спідниця", "Пальто", "Сорочка", "Джинси", "Шорти", "Блузка", "Піджак",
        "Комбінезон", "Капелюх", "Шапка", "Рукавички", "Шарф", "Плаття", "Брюки", "Майка", "Туфлі", "Босоніжки", "Чоботи", "Черевики", "Кросівки", "Жилет", "Носки"
    };

        private static readonly List<string> ElectronicsKeywords = new List<string>
    { "Смартфон", "Ноутбук", "Планшет", "Телевізор", "Монітор", "Навушники", "Колонки", "Камера", "Фотокамера", "Мікрофон", "Принтер", "Сканер", "Мишка",
        "Клавіатура", "Ігрова консоль", "Роутер", "Смарт-годинник", "Електронна книга", "Проектор", "Веб-камера", "Павербанк", "Зарядний пристрій", "USB кабель",
        "SSD диск", "Жорсткий диск", "Процесор", "Відеокарта", "Материнська плата", "Оперативна пам'ять", "Блок живлення"
    };

        private static readonly List<string> DocumentsKeywords = new List<string>
    {
        "Паспорт", "Свідоцтво про народження", "Посвідчення водія", "Диплом", "Атестат", "Заява", "Договір", "Контракт", "Резюме", "Сертифікат", "Ліцензія",
        "Медична картка", "Рахунок", "Квитанція", "Податкова декларація", "Звіт", "Протокол", "Нотаріальний акт", "Витяг", "Запрошення", "Повідомлення", "Віза",
        "Митна декларація", "Ідентифікаційний код", "Страховий поліс", "Посвідчення особи", "Свідоцтво про шлюб", "Трудова книжка", "Реєстраційне свідоцтво",
        "Посвідчення учасника бойових дій"
    };

        public static string Category(List<string> titleWords, List<string> descriptionWords)
        {
            Dictionary<string, int> categoryCounts = new Dictionary<string, int>
        {
            {"Біжутерія", 0},
            {"Одяг", 0},
            {"Електроніка", 0},
            {"Документи", 0}
        };

            // Check title and description words against each keyword list
            CheckKeywords(titleWords, BijouterieKeywords, categoryCounts, "Біжутерія");
            CheckKeywords(descriptionWords, BijouterieKeywords, categoryCounts, "Біжутерія");

            CheckKeywords(titleWords, ClothingKeywords, categoryCounts, "Одяг");
            CheckKeywords(descriptionWords, ClothingKeywords, categoryCounts, "Одяг");

            CheckKeywords(titleWords, ElectronicsKeywords, categoryCounts, "Електроніка");
            CheckKeywords(descriptionWords, ElectronicsKeywords, categoryCounts, "Електроніка");

            CheckKeywords(titleWords, DocumentsKeywords, categoryCounts, "Документи");
            CheckKeywords(descriptionWords, DocumentsKeywords, categoryCounts, "Документи");
            var maxCategory = categoryCounts.OrderByDescending(c => c.Value).First();

            // If the highest count is 0, return "Невизначена" category
            if (maxCategory.Value == 0)
            {
                return "Невизначена";
            }

            return maxCategory.Key;
            // Determine the category with the highest count
          // return categoryCounts.OrderByDescending(c => c.Value).First().Key;
        }

        private static void CheckKeywords(List<string> words, List<string> keywords, Dictionary<string, int> categoryCounts, string category)
        {
            foreach (var word in words)
            {
                if (keywords.Any(keyword => keyword.Equals(word, StringComparison.OrdinalIgnoreCase)))
                {
                    categoryCounts[category]++;
                }
            }
        }
    }

}
