namespace Cosmetic_Finder.TUI
{
    public static class Categories
    {
        public static short GettingCategory()
        {
            Console.WriteLine("Wybierz kategorię, podając cyfrę od 1 do 10: ");
            var counter = 0;
            foreach (var category in Common.Domain.Model.Categories.CosmeticCategories)
            {
                counter++;
                Console.WriteLine($"{counter}: {category.Value}");
            }

            short categoryNum;
            try
            {
                categoryNum = Convert.ToInt16(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Podaj poprawną wartość");
                categoryNum = Convert.ToInt16(Console.ReadLine());
            }
            
            return categoryNum;
        }
        public static int CategoryOptions(short categoryNum)
        {
            var mainCategoryId = 0;

            switch (categoryNum)
            {
                case 1:
                    mainCategoryId = 8686;
                    break;
                case 2:
                    mainCategoryId = 8528;
                    break;
                case 3:
                    mainCategoryId = 8655;
                    break;
                case 4:
                    mainCategoryId = 8625;
                    break;
                case 5:
                    mainCategoryId = 8576;
                    break;
                case 6:
                    mainCategoryId = 9220;
                    break;
                case 7:
                    mainCategoryId = 8512;
                    break;
                case 8:
                    mainCategoryId = 8471;
                    break;
                case 9:
                    mainCategoryId = 9246;
                    break;
                case 10:
                    mainCategoryId = 8445;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }
            return mainCategoryId;
        }
    }
}
