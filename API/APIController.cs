using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UIMock.Entities;
using Newtonsoft.Json;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Xml.Linq;

namespace UIMock.API
{

    public class APIController
    {
        private List<Cantines> Cantines = new List<Cantines>();
        private readonly HttpClient _httpClient;
        private string url = "http://192.168.0.11:8080";
        public APIController()
        {
            _httpClient = new HttpClient();
            LoadCantines();
        }


        public async Task<Cantines> GetCantine(List<Cantines> cantines, int CantineID)
        {
            foreach (Cantines cant in cantines)
            {
                if (cant.Id == CantineID)
                {
                    return cant;
                }
            }
            return null;
        }

        public async Task LoadUser(string userid)
        {
            try
            {
                var response = await _httpClient.GetAsync(url+"/api/userdata/"+userid);
                if (response != null && response.IsSuccessStatusCode)
                {
                    var responseBodyJson = await response.Content.ReadAsStringAsync();
                    dynamic responseData = JsonConvert.DeserializeObject(responseBodyJson);
                    string name = responseData.Name;
                    string email = responseData.Email;
                    string phone = responseData.Phone;
                    string subid = responseData.sub_id;
                    string subitemid = responseData.sub_item_id;
                    int location = Convert.ToInt32(responseData.Location);
                    Cantines cant = await GetCantine(Cantines, location);
                    User.CurrentUser = new User(userid, name, email, phone, subid, subitemid, cant);
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateUser(string UserID, string Name, string Email, string Phone, int CantineId)
        {
            try
            {
                var values = new Dictionary<string, string>
                {
                    {"UserID", UserID},
                    {"Name", Name},
                    {"Email", Email},
                    {"Phone", Phone},
                    {"Location", CantineId.ToString() }
                };
                var content = new FormUrlEncodedContent(values);

                var response = await _httpClient.PostAsync(url + "/api/signup", content);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task LoadCantines()
        {
            try
            {
                var response = await _httpClient.GetAsync(url + "/api/locations");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var cantines = JsonConvert.DeserializeObject<List<Cantines>>(json);
                    Cantines = cantines;
                }
                else
                {
                    throw new Exception($"Failed to get cantines: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading cantines: {ex.Message}");
            }
        }

        public async Task<List<Cantines>>GetCantines()
        {
            if(Cantines.Count == 0)
            {
                await LoadCantines();
            }
            return Cantines;
        }

        public async Task<Result.ResultCode> UpdateSubscription(string UserID, string SubId, string SubItemId)
        {
            try
            {
                var values = new Dictionary<string, string>
                {
                    {"userid", UserID},
                    {"subid", SubId},
                    {"subitemid", SubItemId}
                };
                var content = new FormUrlEncodedContent(values);

                var response = await _httpClient.PostAsync(url + "/api/update_sub", content);

                if (response.IsSuccessStatusCode)
                {
                    return Result.ResultCode.Success;
                } else
                {
                    return Result.ResultCode.Failure;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
