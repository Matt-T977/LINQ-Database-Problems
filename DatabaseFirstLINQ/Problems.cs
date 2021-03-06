using DatabaseFirstLINQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        //Completed
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var users = _context.Users.ToList().Count;
            Console.WriteLine(users);
        }

        //Completed
        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        //Completed
        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;
            var productsOver150 = products.Where(p => p.Price > 150);
            foreach (var product in productsOver150)
            {
                Console.WriteLine(product.Name + " " + product.Price);
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products;
            var productsWithS = products.Where(p => p.Name.Contains("s"));
            foreach (var product in productsWithS)
            {
                Console.WriteLine(product.Name);
            }
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            var users = _context.Users;
            var usersPre2016 = users.Where(u => u.RegistrationDate < new DateTime(2016, 1, 1)).ToList();
            foreach (User user in usersPre2016)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var users = _context.Users;
            var usersPre2016 = users.Where(u => u.RegistrationDate >= new DateTime(2017, 1, 1) && u.RegistrationDate < new DateTime(2018, 1, 1)).ToList();
            foreach (User user in usersPre2016)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User)
                .Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var userCart = _context.ShoppingCarts.Include(p => p.Product).Include(u => u.User)
                .Where(u => u.User.Email == "afton@gmail.com");
            foreach (ShoppingCart cart in userCart)
            {
                Console.WriteLine($"Product Name: {cart.Product.Name} Price: {cart.Product.Price} Quantity: {cart.Quantity}");
            }

        }

        //Completed
        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var customerTotal = _context.ShoppingCarts.Include(p => p.Product).Include(p => p.User)
                .Where(p => p.User.Email == "oda@gmail.com").Select(p => p.Product.Price).Sum();
            Console.WriteLine(customerTotal);

        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            //Solution One (Matt)
            var employees = _context.UserRoles.Where(ur => ur.RoleId == 2).Select(ur => ur.UserId);
            var shoppingCarts = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product);
            var employeeCarts = shoppingCarts.Where(sc => employees.Contains(sc.UserId));

            foreach (ShoppingCart cart in employeeCarts)
            {
                Console.WriteLine($"Employee: {cart.User.Email} Product: {cart.Product.Name} Cost: {cart.Product.Price} Quantity: {cart.Quantity}");
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProdut = new Product()
            {
                Name = "Apple Airpods with Charging Case",
                Description = "Bluetooth headphones made by Apple in California",
                Price = 119.00m
            };
            _context.Products.Add(newProdut);
            _context.SaveChanges();
            Console.WriteLine("Product Added Successfully");
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Apple Airpods with Charging Case").Select(p => p.Id).SingleOrDefault();
            var customerId = _context.Users.Where(cn => cn.Email == "david@gmail.com").Select(ci => ci.Id).SingleOrDefault();
            ShoppingCart newCart = new ShoppingCart()
            {
                UserId = customerId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Price == 119.00m).SingleOrDefault();
            product.Price = 135.99m;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var userId = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(userId);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Email Address: ");
            var userEmail = Console.ReadLine();
            Console.WriteLine("Password: ");
            var userPass = Console.ReadLine();
            var checkUserEmailExist = _context.Users.Where(eu => eu.Email == userEmail).SingleOrDefault();
            var checkUserPassExist = _context.Users.Where(eu => eu.Password == userPass).SingleOrDefault();
            if (checkUserEmailExist == null || checkUserPassExist == null)
            {
                Console.WriteLine("Invalid Email or Password");
            }
            else if (userEmail == checkUserEmailExist.Email && userPass == checkUserPassExist.Password)
            {
                Console.WriteLine("Signed in!");
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.
            var customers = _context.Users.ToList();
            var subTotal = 0.0m;
            foreach (User customer in customers)
            {
                var customerTotal = _context.ShoppingCarts.Include(p => p.Product).Include(p => p.User)
                    .Where(p => p.User.Email == customer.Email).Select(p => p.Product.Price).Sum();

                Console.WriteLine($"Customer: {customer.Email} Total:{customerTotal}");
                subTotal += customerTotal;
            }
            Console.WriteLine($"Total Cart Value:{subTotal}");

        }

        // BIG ONE
        public string userLogIn()
        {
            bool successLogin = false;
            var userEmail = "";
            while (successLogin == false) {
                Console.WriteLine("Email Address: ");
                userEmail = Console.ReadLine();
                Console.WriteLine("Password: ");
                var userPass = Console.ReadLine();
                var checkUserEmailExist = _context.Users.Where(eu => eu.Email == userEmail).SingleOrDefault();
                var checkUserPassExist = _context.Users.Where(eu => eu.Password == userPass).SingleOrDefault();
                if (checkUserEmailExist == null || checkUserPassExist == null)
                {
                    Console.WriteLine("Invalid Email or Password");

                }
                else if (userEmail == checkUserEmailExist.Email && userPass == checkUserPassExist.Password)
                {
                    Console.WriteLine("Signed in!");
                    successLogin = true;
                    
                }
            }
            return userEmail;
        }

        public void showAllProductsCart(string userEmail)
        {
            var userCart = _context.ShoppingCarts.Include(sc => sc.Product).Where(sc => sc.User.Email == userEmail);
            foreach (var product in userCart)
            {
                Console.WriteLine($"Product: {product.Product.Id}-{product.Product.Name} | Price: ${product.Product.Price}.00 Quantity: | {product.Quantity}\n");
            }
        }

        public void showAllProducts()
        {
            var allProducts = _context.Products.ToList();
            foreach (Product product in allProducts)
            {
                Console.WriteLine($"Product: {product.Id}-{product.Name} | Price: ${product.Price}.00 | Description: {product.Description}");
            }
        }

        public void addProductToCart(string userEmail)
        {
            Console.WriteLine("Please enter the number of the product you would like to add to your cart.");
            string userInput = Console.ReadLine();
            int input = Int32.Parse(userInput);

            var productId = _context.Products.Where(p => p.Id == input).Select(p => p.Id).SingleOrDefault();
            var customerId = _context.Users.Where(cn => cn.Email == userEmail).Select(ci => ci.Id).SingleOrDefault();
            var customerCart = _context.ShoppingCarts.Where(p => p.ProductId == productId).Where(p => p.UserId == customerId).SingleOrDefault();

            if(customerCart == null)
            {
                ShoppingCart newCart = new ShoppingCart()
                {
                    UserId = customerId,
                    ProductId = productId,
                    Quantity = 1
                };
                _context.ShoppingCarts.Add(newCart);
                _context.SaveChanges();
            }
            else
            {
                customerCart.Quantity += 1;
                _context.SaveChanges();
            }

        }

        public void removeProductFromCart(string userEmail)
        {
            Console.WriteLine("Please enter the number of the product you would like to remove from your cart.");
            string userInput = Console.ReadLine();
            int input = Int32.Parse(userInput);

            var productId = _context.Products.Where(p => p.Id == input).Select(p => p.Id).SingleOrDefault();
            var customerId = _context.Users.Where(cn => cn.Email == userEmail).Select(ci => ci.Id).SingleOrDefault();
            var deleteRow = _context.ShoppingCarts.Where(p => p.ProductId == productId).Where(p => p.UserId == customerId).SingleOrDefault();
            
            _context.ShoppingCarts.Remove(deleteRow);
            _context.SaveChanges();

        }
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console - -
            // 2. If the user succesfully signs in - -
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sign in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials
            string userEmail = userLogIn();
            Console.WriteLine("\n Please Select an option from one of the following:\n" +
                "1: View all Products in your cart.\n" +
                "2: View all products on our website.\n" +
                "3: Add a product to your shopping cart.\n" +
                "4: Remove a product from your shopping cart.\n");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    showAllProductsCart(userEmail);
                    break;
                case "2":
                    showAllProducts();
                    break;
                case "3":
                    showAllProducts();
                    addProductToCart(userEmail);
                    break;
                case "4":
                    showAllProductsCart(userEmail);
                    removeProductFromCart(userEmail);
                    break;
                default:
                    BonusThree();
                    break;
            }

        }

    }
}
