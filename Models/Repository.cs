namespace FormsApp.Models
{
    public class Repository{
        private static readonly List<Product> _products = new List<Product>();
        private static readonly List<Category> _categories = new List<Category>();


        static Repository(){
            _categories.Add(new Category{CategoryId=1, Name="Phone"});
            _categories.Add(new Category{CategoryId=2, Name="Computer"});

            _products.Add(new Product{ProductId=1, Name="Iphone 14", Image="iphone14.jpeg", Price=40000, CategoryId=1, IsActive=true });
            _products.Add(new Product{ProductId=2, Name="Iphone 15", Image="iphone15.jpeg", Price=50000, CategoryId=1, IsActive=false });

            _products.Add(new Product{ProductId=3, Name="Iphone 15 Pro", Image="iphone15pro.jpeg", Price=60000, CategoryId=1, IsActive=true });

            _products.Add(new Product{ProductId=4, Name="Iphone SE", Image="iphonese.jpeg", Price=70000, CategoryId=1, IsActive=true });
            _products.Add(new Product{ProductId=5, Name="MacBook Air", Image="MacBookAir.jpg", Price=80000, CategoryId=2, IsActive=false });
            _products.Add(new Product{ProductId=6, Name="MacBookPro", Image="MacBookPro.png", Price=90000, CategoryId=2, IsActive=true });
        }
        public static List<Product> Products{
            get{
                return _products;
            }
        }

        public static void CreateProduct(Product product){
            _products.Add(product);
        }

        public static void EditProduct(Product editedProduct){
            var entity = _products.FirstOrDefault(x => x.ProductId == editedProduct.ProductId);  
            if(entity!=null){
                entity.Name = editedProduct.Name;
                entity.Price = editedProduct.Price;
                entity.CategoryId = editedProduct.CategoryId;
                entity.IsActive = editedProduct.IsActive;
                entity.Image=editedProduct.Image;
            } 
        }

        public static void EditIsActive(Product editedProduct){
            var entity = _products.FirstOrDefault(x => x.ProductId == editedProduct.ProductId);  
            if(entity!=null){
                entity.IsActive = editedProduct.IsActive;
            } 
        }
        public static void DeleteProduct(Product deletedProduct){
            var entity = _products.FirstOrDefault(x=> x.ProductId == deletedProduct.ProductId);
            if(entity!=null){
                _products.Remove(entity);
            }
        }
        public static List<Category> Categories{
            get{
                return _categories;
            }
        }
    }
}