using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessServices;
using BusinessEntites;

namespace WebApi1.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productService)
        {
            this._productServices = productService;
        }
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
        public HttpResponseMessage Get(int id)
        {
            var product = _productServices.GetProductById(id);
            if (product != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "No Product found for this Id");
        }

        public int Post([FromBody]ProductEntity productEntity)
        {
            return _productServices.CreateProduct(productEntity);
        }

        public bool Put(int id, [FromBody]ProductEntity productEntity)
        {
            if (id > 0)
            {
                return _productServices.UpdateProduct(id, productEntity);
            }
            return false;
        }

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
