using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using TimeDev.Models.Dto;
using TimeDev.Services;
using Xamarin.Forms;

namespace TimeDev.Models
{
    public class ManageEngineTrackerType : ITrackerType
    {
        private AppSettingsManager Settings => DependencyService.Get<AppSettingsManager>();
        public string Name => "ManageEngine 10";
        public int Index { get; set; }

        public async Task<ResponceRequestsDto> GetRequestsAsync(Tracker tracker)
        {
            var requestURL = new Uri(new Uri(tracker.URL), "/api/v3/requests").OriginalString;

            ResponceRequestsDto responseEntity = null;
            string jsonstr = @"{
                                    'list_info': {
                                        'start_index': 1,
                                        'sort_field': 'subject',
                                        'sort_order': 'asc',
                                        'get_total_count': true,
                                        'filter_by': {
                                            'name': 'All_Pending_User'
                                        },
                                        'fields_required': [
                                            'id',
                                            'subject',
                                            'technician'
                                        ]
                                    }
                                }".Replace(" ", "").Replace("\r\n", "");

            string getparamstr = HttpUtility.UrlEncode(jsonstr);
            string urlstr = $"{requestURL}?input_data={getparamstr}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlstr),
            };
            request.Headers.TryAddWithoutValidation("TECHNICIAN_KEY", tracker.APIkey);
            try
            {
                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var str = response.Content.ReadAsStringAsync();
                    responseEntity = JsonConvert.DeserializeObject<ResponceRequestsDto>(str.Result);
                }

                request.Dispose();
                response.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return responseEntity;
        }

        public async Task<ResponceTasksDto> GetTasksAsync(Tracker tracker)
        {
            var requestURL = new Uri(new Uri(tracker.URL), "/api/v3/tasks").OriginalString;

            ResponceTasksDto responseTasks = null;
            string jsonstr = @"{
                                    'list_info': {
                                        'filter': '1'
                                    },
                                    'fields_required': [
                                        'id',
                                        'title',
                                        'description',
                                        'owner'
                                    ]
                                }".Replace(" ", "").Replace("\r\n", "");

            string getparamstr = HttpUtility.UrlEncode(jsonstr);
            string urlstr = $"{requestURL}?input_data={getparamstr}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlstr),
            };
            request.Headers.TryAddWithoutValidation("TECHNICIAN_KEY", tracker.APIkey);
            try
            {
                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var str = response.Content.ReadAsStringAsync();
                    responseTasks = JsonConvert.DeserializeObject<ResponceTasksDto>(str.Result);
                }

                request.Dispose();
                response.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return responseTasks;
        }

        HttpClient httpClient;

        public ManageEngineTrackerType()
        {
            // create a separate handler for use in this controller
            var handler = new HttpClientHandler();

            // add a custom certificate validation callback to the handler
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, errors) => { return true; };

            // create an HttpClient that will use the handler
            httpClient = new HttpClient(handler);
        }

        public async Task<List<ITaskLocal>> LoadTasks(Tracker tracker)
        {
            List<ITaskLocal> tasks = new List<ITaskLocal>();
            ResponceRequestsDto responseEntity = await GetRequestsAsync(tracker);
            ResponceTasksDto responseTasks = await GetTasksAsync(tracker);
            tasks.AddRange(responseEntity?.Request);
            tasks.AddRange(responseTasks?.Tasks);

            return tasks;
        }

        public async Task<HttpStatusCode> SendTask(TaskLocalDto taskLocalDto)
        {
            var tracker = Settings.UserSettings.InstanceList[taskLocalDto.InstanceIndex];
            var requestURL = new Uri(new Uri(tracker.URL), "/api/v3/worklog").OriginalString;

            string jsonstr = $@"{{
                                    'worklog':{{
                                        '{taskLocalDto.TaskLocal.TaskType}':{{
                                            'id':'{taskLocalDto.TaskLocal.Id}'
                                        }},
                                        'description':'{taskLocalDto.TaskLocal.Comment}',
                                        'technician':{{
                                            'id':'{taskLocalDto.TaskLocal.User.Id}'
                                        }},
                                        'start_time':{{
                                            'value':{taskLocalDto.TaskLocal.BeginTimeTask.Value.ToUnixTimeMilliseconds()}
                                        }},
                                        'end_time':{{
                                            'value':{(taskLocalDto.TaskLocal.BeginTimeTask.Value.ToUnixTimeMilliseconds() + (long)taskLocalDto.TaskLocal.Duration.TotalMilliseconds)}
                                        }}
                                    }}
                                }}".Replace("  ", "").Replace("\r\n", "");
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(jsonstr), "input_data");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestURL),
                Content = formData
            };
            request.Headers.TryAddWithoutValidation("TECHNICIAN_KEY", tracker.APIkey);
            request.Headers.Add("Accept", "application/json");
            HttpStatusCode rezult = HttpStatusCode.BadRequest;
            try
            {
                var response = await httpClient.SendAsync(request);
                rezult = response.StatusCode;
                request.Dispose();
                response.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return await Task.FromResult(rezult);
        }
    }
}
