using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HousePriceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousePriceController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var housePriceSample1 = new HouseData() { Area = "76", BedRooms = 3, BedRoomsBsmt = 0, FullBath = 2, HalfBath = 0, Rooms = 7, ApproxSquFeet = 1300, GarageType = "Attached", GarageSpaces = 2 };
            var price = HousePricePrediction.PredictSinglePrice(housePriceSample1, @"MLNETModels\housePriceModel.zip");

            return new string[] { price };
        }

        //api/HousePrice?area=76&sqft=1300&rooms=7&bedrooms=3&fullBath=2&halfbath=0&garageType=Attached&garageSpaces=2
         [HttpGet("{area}/{sqft}/{rooms}/{bedrooms}/{fullbath}/{halfbath}/{garageType}/{garageSpaces}")]
        public ActionResult<string> Get(string area, int sqft, int rooms, int fullbath, int halfbath, string garageType, int garageSpaces)
        {
            return "value";
        }
    }
}