using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using demo_simple_ion_cms.Constants;
using Firebase.Database;
using Firebase.Database.Query;

namespace demo_simple_ion_cms.Utils
{
    public class FirebaseUtil
    {
        private static FirebaseClient _firebaseClient;
        
        private static void InitFirebaseClient()
        {
            _firebaseClient = new FirebaseClient(GlobalConstants.FIREBASE_DATABASE_URL);
        }

        public static async Task<T> Update<T>(string child, T targetObject)
        {
            InitFirebaseClient();

            await _firebaseClient
                .Child(child)
                .PutAsync(targetObject);

            return targetObject;
        }
        
        public static async Task<T> Create<T>(string child, T targetObject)
        {
            InitFirebaseClient();

            await _firebaseClient
                .Child(child)
                .PostAsync(targetObject);

            return targetObject;
        }
        
        public static async Task<T> Get<T>(string child)
        {
            InitFirebaseClient();

            var response = await _firebaseClient
                .Child(child)
                .OnceSingleAsync<T>();
            
            return response;
        }
    }
}