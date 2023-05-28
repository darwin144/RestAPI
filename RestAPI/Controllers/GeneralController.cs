using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Others;
using RestAPI.ViewModels.Bookings;
using RestAPI.ViewModels.Rooms;
using System;
using System.Net;

namespace RestAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController<TEntity, TEntityVM> : ControllerBase  
        
    {
        private readonly IGeneralRepository<TEntity> _generalRepository;
        private readonly IMapper<TEntity, TEntityVM> _mapper;

        private readonly ResponseVM<TEntityVM> respons = new ResponseVM<TEntityVM>();

        public GeneralController(IGeneralRepository<TEntity> generalRepository, IMapper<TEntity, TEntityVM> mapper)
        {
            _generalRepository = generalRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create(TEntityVM entity)
        {
            try
            {
                var objectConverted = _mapper.Map(entity);
                var result = _generalRepository.Create(objectConverted);

                if (result is null)
                {
                    return NotFound(new ResponseVM<TEntity>
                    {
                        Code = 400,
                        Status = "Failed",
                        Message = "Not Found",
                        Data = result
                    }
                    );

                }
                return Ok(new ResponseVM<TEntity>
                {
                    Code = 200,
                    Status = "OK",
                    Message = "Success",
                    Data = result
                }
              );
               
            }
            catch(Exception ex) {
                return Ok(new ResponseVM<TEntity>
                {
                    Code = 500,
                    Status = "Erorr",
                    Message = ex.Message
                }
              );
            }
        }
        [HttpPut]
        public IActionResult Update(TEntityVM bookingVM)
        {
            try
            {
                var booking = _mapper.Map(bookingVM);
                var isUpdated = _generalRepository.Update(booking);
                if (!isUpdated)
                {
                    return NotFound(new ResponseVM<TEntity>
                    {
                        Code = 400,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Gagal Update Data",
                    }
                  );
                }

                return Ok(new ResponseVM<TEntity>
                {
                    Code = 200,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Success"

                }
              );

            }
            catch (Exception ex) {
                return Ok(new ResponseVM<TEntity>
                {
                    Code = 500,
                    Status = "Erorr",
                    Message = ex.Message
                }
               );
             }

        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var isDeleted = _generalRepository.Delete(guid);
                if (!isDeleted)
                {
                    return NotFound(new ResponseVM<TEntity>
                    {
                        Code = 400,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Gagal Delete Data",
                    }
                  );
                }

                return Ok(new ResponseVM<TEntity>
                {
                    Code = 200,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Success"

                }
              );
            }
            catch (Exception ex) {
                return BadRequest(new ResponseVM<TEntity>
                {
                    Code = 500,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = ex.Message

                }
          );

            }
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            try
            {
                var Entityresult = _generalRepository.GetByGuid(guid);
                if (Entityresult is null)
                {
                    return NotFound(respons.NotFound());
                    /*                    return NotFound(new ResponseVM<TEntity>
                                        {
                                            Code = 400,
                                            Status = HttpStatusCode.NotFound.ToString(),
                                            Message = "Not Found",
                                            Data = Entityresult
                                        }
                                     );
                    */
                }
                var bookingConverted = _mapper.Map(Entityresult);

                return Ok(respons.Success(bookingConverted));
                
            }
            catch(Exception ex) {

                return Ok(respons.Error(ex.Message));
                /*return BadRequest(new ResponseVM<TEntityVM>
                {
                    Code = 500,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = ex.Message
                }
                 );*/
            }       
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var respons = new ResponseVM<IEnumerable<TEntityVM>>();
            try
            {
                var entities = _generalRepository.GetAll();
                if (!entities.Any())
                {
                    return NotFound(respons.NotFound());
                }
                var entityConverteds = entities.Select(_mapper.Map).ToList();

                return Ok(respons.Success(entityConverteds));

            }
            catch (Exception ex) {

                return NotFound(respons.Error(ex.Message));

            }
        }
    }
}
