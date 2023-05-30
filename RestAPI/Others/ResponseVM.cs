using Azure;
using RestAPI.ViewModels.Bookings;
using System.Net;

namespace RestAPI.Others
{
    public class ResponseVM<Tentity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public Tentity? Data { get; set; }


        /*public static ResponseVM<Tentity> Successfully(Tentity entity) {
            return new ResponseVM<Tentity>
            {
                Code = 200,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = entity
            };           
        }*/
        public static ResponseVM<Tentity> Successfully(Tentity entity)
        {
            return new ResponseVM<Tentity>
            {
                Code = 200,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = entity
            };

        }

        /*public static ResponseVM<Tentity>Successfully(Tentity keterangan)
        {
            return new ResponseVM<Tentity>
            {
                Code = 200,
                Status = HttpStatusCode.OK.ToString(),
                Message = "success",
                Data = keterangan
                
            };
        }*/
        public static ResponseVM<Tentity> NotFound(string keterangan)
        {
            return new ResponseVM<Tentity>
            {
                Code = 400,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = keterangan
            };
        }
        
        public static ResponseVM<Tentity> NotFound(IEnumerable<Tentity> entity)
        {
            return new ResponseVM<Tentity>
            {
                Code = 400,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Tidak ada"
            };
        }
        public static ResponseVM<Tentity> NotFound(Tentity entity)
        {
            return new ResponseVM<Tentity>
            {
                Code = 400,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Tidak ada"
            };
        }
        public static ResponseVM<Tentity> Error(string explain) {
            return new ResponseVM<Tentity>
            {
                Code = 500,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = explain
            };
        }
        /*public ResponseVM<Tentity> Error(string explain)
        {
            return new ResponseVM<Tentity>
            {
                Code = 500,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = explain
            };
        }*/
    }
}
