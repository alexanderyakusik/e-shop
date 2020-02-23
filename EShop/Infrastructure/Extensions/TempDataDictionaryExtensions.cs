using EShop.Models.App;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace EShop.Infrastructure.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        private const string TEMP_DATA_NOTIFICATION_MESSAGE_KEY = "NOTIFICATION_MESSAGE";
        private const string TEMP_DATA_ACTIVE_TAB_KEY = "ACTIVE_TAB";

        public static void AddNotificationMessage(this ITempDataDictionary tempData, Notification notification)
        {
            string json = JsonConvert.SerializeObject(notification);
            tempData[TEMP_DATA_NOTIFICATION_MESSAGE_KEY] = json;
        }

        public static string GetNotificationMessage(this ITempDataDictionary tempData)
        {
            return tempData[TEMP_DATA_NOTIFICATION_MESSAGE_KEY] as string;
        }

        public static void SetActiveTab(this ITempDataDictionary tempData, string activeTab)
        {
            tempData[TEMP_DATA_ACTIVE_TAB_KEY] = activeTab;
        }

        public static string GetActiveTab(this ITempDataDictionary tempData)
        {
            return tempData[TEMP_DATA_ACTIVE_TAB_KEY] as string;
        }
    }
}
