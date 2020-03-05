using System;

using CoronavirusSOS.RestService;

namespace CoronavirusSOS.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Update Item { get; set; }
        public ItemDetailViewModel(Update item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
