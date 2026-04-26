using System;
using MyRepositoryApp.Repositories;
using MyRepositoryApp.Entities;

namespace MyRepositoryApp
{
    class Program
    {
        static void Main()
        {

            var productRepository = new Repository<Product>();

            productRepository.Add(new Product(1, "Телефон", 1000));
            productRepository.Add(new Product(2, "Ноутбук", 2500));

            try
            {
                productRepository.Add(new Product(1, "Смарт-часы", 500));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message); // Ожидается сообщение об ошибке
            }

            var product = productRepository.GetById(1);
            if (product != null)
            {
                Console.WriteLine($"Найден продукт: {product.Name}, цена: {product.Price}");
            }

            // Поиск продуктов дороже 1000
            var expensiveProducts = productRepository.Find(p => p.Price > 1000);
            foreach (var expensiveProduct in expensiveProducts)
            {
                Console.WriteLine($"Дорогой продукт: {expensiveProduct.Name}, цена: {expensiveProduct.Price}");
            }

            bool isRemoved = productRepository.Remove(1);
            Console.WriteLine(isRemoved ? "Продукт удален." : "Продукт не найден.");

            // Создание репозитория для пользователей
            var userRepository = new Repository<User>();
            userRepository.Add(new User(1, "john_doe"));
            userRepository.Add(new User(2, "jane_doe"));

            // Поиск пользователя по ID
            var user = userRepository.GetById(1);
            if (user != null)
            {
                Console.WriteLine($"Найден пользователь: {user.Username}");
            }
        }
    }
}
