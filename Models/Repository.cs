namespace FormsApp.Models
{
    public class Repository{
        private static readonly List<Product> _products = new List<Product>();
        private static readonly List<Category> _categories = new List<Category>();


        static Repository(){
            _categories.Add(new Category{CategoryId=1, Name="Phone"});
            _categories.Add(new Category{CategoryId=2, Name="Computer"});

            _products.Add(new Product{ProductId=1, Name="Iphone 14", Image="iphone14.jpeg", Price=40000, CategoryId=1 });
            _products.Add(new Product{ProductId=2, Name="Iphone 15", Image="iphone15.jpeg", Price=50000, CategoryId=1 });

            _products.Add(new Product{ProductId=3, Name="Iphone 15 Pro", Image="iphone15pro.jpeg", Price=60000, CategoryId=1 });

            _products.Add(new Product{ProductId=4, Name="Iphone SE", Image="iphonese.jpeg", Price=70000, CategoryId=1 });
            _products.Add(new Product{ProductId=5, Name="MacBook Air", Image="MacBookAir.jpg", Price=80000, CategoryId=2 });
            _products.Add(new Product{ProductId=6, Name="MacBookPro", Image="MacBookPro.png", Price=90000, CategoryId=2 });
        }
        public static List<Product> Products{
            get{
                return _products;
            }
        }

        public static void CreateProduct(Product product){
            _products.Add(product);
        }
        public static List<Category> Categories{
            get{
                return _categories;
            }
        }
    }
}