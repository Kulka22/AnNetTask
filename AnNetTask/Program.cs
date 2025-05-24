namespace AnNetTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Формат \"адрес@домен; адрес@домен; адрес@домен\"");
            Console.Write("Введите значение To: ");
            string to = Console.ReadLine();
            Console.Write("Введите значение Copy: ");
            string copy = Console.ReadLine();
            Email email1 = new Email(to, copy);
            Console.WriteLine("Результат после заполнения и фильтрации.");
            Console.Write($"To: {email1.To}\n");
            Console.Write($"Copy: {email1.Copy}\n");
        }
    }
}
