﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gymlocator.Core.Shopping;
using gymlocator.Core.Shopping.Models;
using gymlocator.Rest;
using gymlocator.Rest.Models;
using Microsoft.Rest;
using TinyCacheLib;

namespace gymlocator.Core
{
    public class NoClientCredentials : ServiceClientCredentials
    {

    }

    public class ShoppingService
    {
        private IShoppingAPI _client;

        public ShoppingService()
        {
            var retryHandler = new TinyRetryDelegationHandler();
            retryHandler.RetryOn<Exception>();
            //_client = new ShoppingAPI(new Uri("http://localhost:5000"), new UnsafeCredentials(), new TinyCache.TinyCacheDelegationHandler());
            _client = new ShoppingAPI(new Uri("http://localhost:5000"), new NoClientCredentials(), retryHandler);
        }

        public async Task<IList<ShoppingList>> GetShoppingLists()
        {
            var data = await TinyCache.RunAsync<IList<ShoppingList>>("shoppingLists10", async () => {
                var ret = await _client.GetShoppingListsAsync();
                return ret;
            });
            return data;
        }

        public async Task AddItem(Item item)
        {
            await _client.AddListItemAsync(item);
        }

        public async Task AddList(ShoppingList item)
        {
            await _client.AddShoppingListAsync(item);
        }

        public async Task<IList<Item>> GetListItems(int listId)
        {
            var data = await TinyCache.RunAsync("listItems" + listId, async () => {
                var ret = await _client.GetListItemsAsync(listId);
                return ret;
            });
            return data;
        }

        public async Task UpdateList(ShoppingList shoppingList)
        {
            await _client.UpdateShoppingListAsync(shoppingList.Id, shoppingList);
        }
    }

    public class DataStore
    {
        static Uri apiEndPoint = new Uri("http://f24s-gym-api034.azurewebsites.net/v13/");
        static TinyRetryDelegationHandler retryHandler = new TinyRetryDelegationHandler();
        GymAPI api = new GymAPI(apiEndPoint, new NoClientCredentials(), retryHandler);
        private string locale = System.Globalization.CultureInfo.CurrentCulture.Name;

        XamarinPropertyStorage store = new XamarinPropertyStorage();

        public DataStore()
        {
            
            retryHandler.RetryOn<Exception>();


            api.BaseUri = apiEndPoint;
            store.LoadFromString(CacheResources.PreloadData.JsonData);

            var preloadString = store.GetAllAsLoadableString();

            TinyCache.SetCacheStore(store);
            TinyCache.OnError += (sender, e) =>
            {
                var i = 3;
            };

            TinyCache.SetBasePolicy(
                new TinyCachePolicy()
                .SetMode(TinyCacheModeEnum.CacheFirst)
                .SetFetchTimeout(TimeSpan.FromSeconds(5)) // 5 second excecution limit
                .SetExpirationTime(TimeSpan.FromMinutes(10)) // 10 minute expiration before next fetch
                .SetUpdateCacheTimeout(50) // Wait 50ms before trying to update cache in background
                );
        }

        public async Task<IList<Gym>> GetGymsAsync()
        {
            //var result = await api.GetGymsAsync(locale) as IList<Gym>;
            var result = await TinyCache.RunAsync("gyms", () => { return api.GetGymListAsync(locale); });
            return result;
        }
    }
}
