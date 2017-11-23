using Bridge.Html5;
using Bridge.jQuery2;
using Bridge1.comp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;


namespace Bridge1
{
    class CompLoader
    {
        private HTMLElement _element;
        Component _comp;

        public CompLoader()
        {
        }
        public void Load(HTMLElement element, Component comp)
        {
            _comp = comp;
            _element = element;

            var config = new AjaxOptions
            {

                Url = _comp.Markup,

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
                    var div = new HTMLDivElement();
                    var s = data.ToString();
                    var pattern = @"{{([a-zA-Z0-9_]+)}}";
                    var s2 = Regex.Replace(s, pattern, "<as_replace text=\"$1\"></as_replace>");
                    div.InnerHTML = s2;
                    _element.AppendChild(div);
                    var bs = div.QuerySelectorAll("[model]");
                    var inputs = div.GetElementsByTagName("input");
                    //inputs.Where(x => x.HasAttribute("model")).DoAction((e) => { e.OnInput = (y) => { var c = 2; }; });
                    foreach (var input in inputs)
                    {
                        var m = input.GetAttribute("model");
                        var inEl = input as HTMLInputElement;
                        (_comp as INotifyPropertyChanged).PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == m)
                            {
                                inEl.Value = sender.GetType().GetProperty(args.PropertyName).GetValue(sender).ToString();
                            }
                        };
                        input.OnInput = (e) =>
                        {
                            var val = (input as HTMLInputElement).Value;
                            var p = _comp.GetType().GetProperty(m);
                            p.SetValue(_comp, val);
                        };
                    }

                    var reps = div.GetElementsByTagName("as_replace");
                    foreach (var rep in reps)
                    {
                        var m = rep.GetAttribute("text");
                        (_comp as INotifyPropertyChanged).PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == m)
                            {
                                rep.TextContent = sender.GetType().GetProperty(args.PropertyName).GetValue(sender).ToString();
                            }
                        };
                    }
                }
            };

            // Make the Ajax request
            jQuery.Ajax(config);

        }
    }
}
