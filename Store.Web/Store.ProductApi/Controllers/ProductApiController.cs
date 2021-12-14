using Microsoft.AspNetCore.Mvc;
using Store.ProductApi.Models.Dtos;
using Store.ProductApi.Repository;

namespace Store.ProductApi.Controllers
{
    [Route("api/products")]
    public class ProductApiController : ControllerBase
    {
        //Fields
        private readonly IProductRepository _repository;

        //Constructor
        public ProductApiController(IProductRepository repository)
        {
            _repository = repository;
        }

        //Methods
        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            var response = new ResponseDto();
            try
            {
                var products = await _repository.GetProducts();
                response.Result = products;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.Message };
            }

            return response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDto> Get(int id)
        {
            var response = new ResponseDto();
            try
            {
                var product = await _repository.GetProductById(id);
                response.Result = product;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.Message };
            }

            return response;
        }

        [HttpPost]
        public async Task<ResponseDto> Post([FromBody] ProductDto productDto)
        {
            var response = new ResponseDto();
            try
            {
                var product = await _repository.CreateUpdateProduct(productDto);
                response.Result = product;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.Message };
            }

            return response;
        }


        [HttpPut]
        public async Task<ResponseDto> Put([FromBody] ProductDto productDto)
        {
            var response = new ResponseDto();
            try
            {
                var product = await _repository.CreateUpdateProduct(productDto);
                response.Result = product;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.Message };
            }

            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            var response = new ResponseDto();
            try
            {
                var result = await _repository.DeleteProduct(id);
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.Message };
            }

            return response;
        }

    }
}
