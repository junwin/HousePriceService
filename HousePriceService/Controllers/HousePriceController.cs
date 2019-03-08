using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using System;
using System.IO;

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
            var price = test();
            return new string[] { price };
        }

        private string test()
        {
            // Run a few test examples
            var housePriceSample1 = new HouseData() { Area = "76", BedRooms = 3, BedRoomsBsmt = 0, FullBath = 2, HalfBath = 0, Rooms = 7, ApproxSquFeet = 1300, GarageType = "Attached", GarageSpaces = 2 };
            return PredictSinglePrice(housePriceSample1);

        }

        public static string PredictSinglePrice(HouseData houseData, string outputModelPath = @"C:\Users\junwi\Source\Repos\HousePriceService\HousePriceService\bin\Debug\netcoreapp2.2\MLNETModels\housePriceModel.zip")
        {
            //  Load the prediction model we saved earlier
            MLContext mlContext = new MLContext(seed: 0);
            ITransformer loadedModel;
            using (var stream = new FileStream(outputModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                loadedModel = mlContext.Model.Load(stream);
            }

            // Creete a handy function based on our HouseData class and a class to contain the result
            var predictionFunction = loadedModel.CreatePredictionEngine<HouseData, HousePrediction>(mlContext);

            // Predict the Sale price - TA DA
            var prediction = predictionFunction.Predict(houseData);

            var pv = prediction.SoldPrice;
            return string.Format("Predicted Price is {0}", pv);

            
        }
    }
}