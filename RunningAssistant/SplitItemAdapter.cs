using System;
using System.Linq;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;

namespace RunningAssistant
{
	public class SplitItemAdapter : BaseAdapter<SplitItem> {
		List<SplitItem> items;
		Activity context;

		public SplitItemAdapter(Activity context, IEnumerable<SplitItem> items)
			: base()
		{
			this.context = context;
			this.items = items.ToList();
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override SplitItem this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate(Resource.Layout.SplitItemView, null);
			view.FindViewById<TextView> (Resource.Id.Distance).Text = string.Format("{0}m",item.Distance.ToString ());
			view.FindViewById<TextView> (Resource.Id.Time).Text = string.Format ("{0}:{1}:{2}", item.Hours, item.Minutes, item.Seconds);
			return view;
		}
	}
}

