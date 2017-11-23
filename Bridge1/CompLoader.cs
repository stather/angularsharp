using Bridge.Html5;
using Bridge.jQuery2;
using Bridge1.comp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Bridge1
{
    class CompLoader
    {
        private HTMLElement _element;
        object _comp;
        HTMLInputElement inEl;

        public CompLoader()
        {
        }
        private void Person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
            Console.WriteLine(" - Old Value: " + e.OldValue);
            Console.WriteLine(" - New Value: " + e.NewValue);
            inEl.Value = e.NewValue.ToString();
        }
        public void Load(HTMLElement element, object comp)
        {
            _comp = comp;
            _element = element;

            var config = new AjaxOptions
            {

                Url = "/comp1/Comp1.html",

                // Serialize the msg into json
                //Data = new { value = JsonConvert.SerializeObject(msg) },

                // Set the contentType of the request
                //ContentType = "application/json; charset=utf-8",
                Error = (jqXHR, d1, d2) =>
                {
                    return;
                },
                // On response, call custom success method
                Success = (data, textStatus, jqXHR) =>
                {
                    // Output the whole response object.
                    //Console.WriteLine(data);
                    var div = new HTMLDivElement();
                    div.InnerHTML = data.ToString();
                    _element.AppendChild(div);
                    var inputs = div.GetElementsByTagName("input");
                    //inputs.Where(x => x.HasAttribute("model")).DoAction((e) => { e.OnInput = (y) => { var c = 2; }; });
                    foreach (var input in inputs)
                    {
                        var m = input.GetAttribute("model");
                        inEl = input as HTMLInputElement;
                        (_comp as INotifyPropertyChanged).PropertyChanged += Person_PropertyChanged;
                        input.OnInput = (e) =>
                        {
                            var val = (input as HTMLInputElement).Value;
                            var p = _comp.GetType().GetProperty(m);
                            p.SetValue(_comp, val);
                            var b = 1;
                        };
                    }
                    // or, output just the message using
                    // the "d" property string indexer.
                    // Console.WriteLine(data["d"]);

                    return;
                }
            };

            // Make the Ajax request
            jQuery.Ajax(config);

        }
    }
}
