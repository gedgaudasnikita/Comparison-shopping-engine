using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_frontend_android
{
    public class SuggestionAdapter: ArrayAdapter
    {
        private SuggestionFilter filter;

        public SuggestionAdapter(Context ctx, int resource) : base(ctx, resource) { }
        
        public override Filter Filter
        {
            get
            {
                if (filter == null)
                {
                    filter = new SuggestionFilter(this);
                }

                return filter;
            }
        }

        private class SuggestionFilter : Filter
        {
            SuggestionAdapter adapter;

            public SuggestionFilter(SuggestionAdapter _adapter)
            {
                adapter = _adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                FilterResults filterResults = new FilterResults();
                Task<List<string>> task;
                List<string> result;

                try
                {
                    task = Task.Run<List<string>>(async () => await BackendInterface.GetSuggestions(constraint.ToString(), 50));
                    result = task.Result;
                }
                catch
                {
                    result = new List<string>();
                }

                filterResults.Values = result.ToArray<string>();
                filterResults.Count = result.Count;
                
                return filterResults;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                var list = results.Values.ToArray<string>();
                if (list != null && list.Length > 0)
                {
                    adapter.Clear();
                    foreach (var t in list)
                    {
                        adapter.Add(t);
                    }
                }

                if (results != null && results.Count > 0)
                {
                    adapter.NotifyDataSetChanged();
                }
                else
                {
                    adapter.NotifyDataSetInvalidated();
                }
            }
        }

    }

}