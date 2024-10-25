namespace ImagesGenerator.WebPortal.ApiControllers
{
    [ApiController]
	public class SubjectAreasController: ControllerBase
    {
        [HttpPost("/api/subject-areas/list")]
        public IActionResult List()
        {
            // curl -X POST https://localhost:7246/api/subject-areas/list
            var result = new[]
            {
                new ListItem("finance", "Финансы", "банки, страхование, инвестиции, кредитование, лизинг"),
                new ListItem("education", "Образование", "школы, курсы, репетиторы, обучение вождению, онлайн-образование"),
                new ListItem("medicine-and-healthcare", "Медицина и здравоохранение", "клиники, стоматологии, лаборатории, аптеки, медицинское оборудование"),
                new ListItem("it-and-technologies", "IT и технологии", "разработка ПО, стартапы, кибербезопасность, веб-дизайн, IT-консалтинг"),
                new ListItem("retail-trade", "Розничная торговля", "одежда, электроника, супермаркеты, бытовая техника, косметика"),
                new ListItem("entertainment", "Развлечения", "кинотеатры, клубы, парки, боулинг, развлекательные центры"),
                new ListItem("beauty-and-health", "Красота и здоровье", "салоны красоты, фитнес-клубы, SPA, массаж, косметология"),
                new ListItem("hospitality-and-tourism", "Гостеприимство и туризм", "отели, рестораны, турфирмы, кафе, гиды"),
                new ListItem("art-and-design", "Искусство и дизайн", "галереи, дизайнеры интерьера, ателье, студии тату, художественные мастерские"),
                new ListItem("construction-and-real-estate", "Строительство и недвижимость", "строительство, агентства недвижимости, архитектурные бюро, ремонт квартир, ландшафтный дизайн"),
                new ListItem("transport-and-logistics", "Транспорт и логистика", "транспортные услуги, склады, курьеры, перевозки, автосервис"),
                new ListItem("food-industry", "Пищевая промышленность", "рестораны, кафе, производство продуктов, кондитерские, булочные"),
                new ListItem("legal-services", "Юридические услуги", "юридические фирмы, консультации, нотариат, регистрация компаний, юридическая помощь"),
                new ListItem("marketing-and-advertising", "Маркетинг и реклама", "реклама, PR, маркетинговые исследования, SMM, брендинг"),
                new ListItem("personal-services", "Личные услуги", "парикмахеры, массажисты, тренеры, няни, домашний мастер"),
                new ListItem("ecology-and-environment", "Экология и окружающая среда", "озеленение, переработка отходов, экомониторинг, экологический консалтинг, садоводство"),
                new ListItem("agriculture", "Сельское хозяйство", "фермерство, агротехника, животноводство, садоводство, пчеловодство"),
                new ListItem("science-and-research", "Наука и исследования", "лаборатории, научные исследования, разработки, изобретения, технический анализ"),
                new ListItem("sport", "Спорт", "спортзалы, тренажерные залы, спортивные клубы, йога-центры, бассейны"),
                new ListItem("music-and-entertainment", "Музыка и развлечения", "концертные залы, музыкальные школы, клубы, диджеи, караоке")
            };

            return new JsonResult(result);
        }

        #region nested classes

		public class ListItem
		{
			public string Value { get; set; } = "";
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";

			public ListItem()
			{ }

			public ListItem(string value, string name, string description)
			{
				Value = value;
				Name = name;
                Description = description;
            }
        }

        #endregion
    }
}