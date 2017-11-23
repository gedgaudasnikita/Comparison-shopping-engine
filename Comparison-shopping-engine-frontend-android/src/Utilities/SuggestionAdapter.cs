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

namespace Comparison_shopping_engine_frontend_android
{
    public class SuggestionAdapter : BaseAdapter, IFilterable
    {
        private Context ctx;
        private int resource;
        private List<string> resultList = new List<string>();
        private SuggestionFilter filter;

        public SuggestionAdapter(Context _ctx, int _resource)
        {
            resource = _resource;
            ctx = _ctx;
        }

        public Filter Filter
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

        public override int Count => resultList.Count();

        public override Java.Lang.Object GetItem(int position)
        {
            return resultList.ElementAt(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var text = convertView.FindViewById<AutoCompleteTextView>(resource);

            text.Text = resultList[position];

            return convertView;
        }
    }

    public class SuggestionFilter: Filter
    {
        SuggestionAdapter adapter;

        public SuggestionFilter(SuggestionAdapter _adapter)
        {
            adapter = _adapter;
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            FilterResults filterResults = new FilterResults();
            var resultTask = BackendInterface.GetSuggestions(constraint.ToString(), 50);
            resultTask.Wait();
            var result = resultTask.Result;

            filterResults.Values = result.ToArray<string>();
            filterResults.Count = result.Count;

            return filterResults;
        }

        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
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