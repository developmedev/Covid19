using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronavirusSOS.RestService;

namespace CoronavirusSOS.Services
{
    public class MockDataStore : IDataStore<Update>
    {
        readonly List<Update> items;
        RestApi RestApi;
        public MockDataStore()
        {
            RestApi = new RestApi();
            items = RestApi.GetUpdatesAsync();
        }

        public async Task<bool> AddItemAsync(Update item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Update item)
        {
            var oldItem = items.Where((Update arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Update arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Update> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Update>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}