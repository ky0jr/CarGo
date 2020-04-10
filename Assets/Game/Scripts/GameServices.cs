using System.Net.Http;
using UnityEditorInternal;

namespace Game.Scripts.Services
{
    public class GameServices
    {
        public static GameServices Instance => _instance ?? (_instance = new GameServices());

        private static GameServices _instance;
        
        private HttpClient client { get; }

        private GameServices()
        {
            client = new HttpClient();
        }
    }
}