using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessServices;
using BusinessEntites;
using AttributeRouting;
using AttributeRouting.Web.Http;

namespace WebApi1.Controllers
{
    [AttributeRouting.RoutePrefix("v1/Products/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productService)
        {
            this._productServices = productService;
        }
        [GET("~/MyRoute/allproducts")]
        [GET("all")]
        public HttpResponseMessage Get()
        {
            var products = _productServices.GetAllProducts();
            if (products != null)
            {
                var productEntites = products as List<ProductEntity> ?? products.ToList();
                if (productEntites.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, productEntites);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }
        [GET("productid/{id?}")]
        [GET("particularproduct/{id?}")]
        [GET("myproduct/{id:range(1,3)}")]
        [GET(@"id/{e:regex(^[0-9]$)}")]
        public HttpResponseMessage Get(int id)
        {
            var product = _productServices.GetProductById(id);
            if (product != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "No Product found for this Id");
        }
        [POST("Create")]
        [POST("Register")]
        public int Post([FromBody]ProductEntity productEntity)
        {
            return _productServices.CreateProduct(productEntity);
        }
        [PUT("Update/prodcutid/{id}")]
        [PUT("Modify/productid/{id}")]
        public bool Put(int id, [FromBody]ProductEntity productEntity)
        {
            if (id > 0)
            {
                return _productServices.UpdateProduct(id, productEntity);
            }
            return false;
        }
        [DELETE("remove/productid/{id}")]
        [DELETE("clear/productid/{id}")]
        [DELETE("delete/productid/{id}")]
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _productServices.DeleteProduct(id);
            }
            return false;
        }
    }
}
