using Microsoft.ML;
using TrackingCodeApi.ml;

namespace TrackingCodeApi.services
{
    public class BatteryPredictionService
    {
        private readonly MLContext _mlContext;

        public BatteryPredictionService()
        {
            _mlContext = new MLContext();
        }

        public BatteryPredictionOutput Prever(BatteryPredictionInput input)
        {
            
            float bateria = 100 - (input.HorasUso * 2)
                                - (input.DiasDesdeUltimaCarga * 4)
                                - ((input.TemperaturaMedia - 25) * 0.3f);

            bateria = Math.Clamp(bateria, 0, 100);

            return new BatteryPredictionOutput { BateriaPrevista = bateria };
        }
    }
}