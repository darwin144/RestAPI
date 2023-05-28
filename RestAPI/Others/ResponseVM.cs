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


        public ResponseVM<Tentity> Success(Tentity entity) {
            return new ResponseVM<Tentity>
            {
                Code = 200,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = entity
            };
            
        }
       
        public ResponseVM<Tentity> Success(string keterangan)
        {
            return new ResponseVM<Tentity>
            {
                Code = 200,
                Status = HttpStatusCode.OK.ToString(),
                Message = keterangan,
            };

        }
        public ResponseVM<Tentity> NotFound(string keterangan)
        {
            return new ResponseVM<Tentity>
            {
                Code = 400,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = keterangan
            };
        }
        public ResponseVM<Tentity> NotFound()
        {
            return new ResponseVM<Tentity>
            {
                Code = 400,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Tidak ada"
            };
        }
        public ResponseVM<Tentity> NotFound(Tentity entity)
        {
            return new ResponseVM<Tentity>
            {
                Code = 400,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Tidak ada"
            };
        }
        public ResponseVM<Tentity> Error(string explain) {
            return new ResponseVM<Tentity>
            {
                Code = 500,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = explain
            };
        }
    }
}
