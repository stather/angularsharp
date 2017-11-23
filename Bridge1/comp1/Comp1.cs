using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Bridge;

namespace Bridge1.comp1
{
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
        AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    public sealed class CanBeNullAttribute : Attribute { }

    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
        AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    public sealed class NotNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        public NotifyPropertyChangedInvocatorAttribute() { }
        public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
        {
            ParameterName = parameterName;
        }

        [CanBeNull] public string ParameterName { get; private set; }
    }

    public abstract class Component : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract string Markup { get;}
    }

    public class Comp1 : Component
    {
        public string Prop2
        {
            get { return _prop2; }
            set
            {
                if (value == _prop2) return;
                _prop2 = value;
                OnPropertyChanged(nameof(Prop2));
            }
        }

        private string _prop2;
        private string _myProp;

        public string MyProp
        {
            get { return _myProp; }
            set
            {
                if (value == _myProp) return;
                _myProp = value;
                OnPropertyChanged(nameof(MyProp));
            }
        }

        public Comp1()
        {
        }


        public override string Markup => "/comp1/Comp1.html";
    }

}
