# MvvmMonkey

MvvmMonkey is a low-overhead library that take a little bit of the boiler-plate (and pain) out of MVVM.

MvvmMonkey is not a framework, it's a collections of features you can pick and choose from, 
it's primary design goal is to have have minimal effect on the application using it.

Currently there's just one version of this library targeting WPF 4.5 (MvvmMonkey.Wpf), if there's demand I'll
port the library to the other XAML platforms (probably just to "Universal Windows App").

Compnents in this library:

Method Binding
---

Adding the '[TypeDescriptionProvider(typeof(MethodBinding))]' attribute to you view model class
will add "fake" ICommand properties for each method that accepts zero or one parameters

The command method should either return void or Task, if you return task ICommand.CanExecute will
return false until the task is complete - disabling the button and preventing the user from 
accedently clickign it again while waiting.

View Model:

    [TypeDescriptionProvider(typeof(MethodBinding))]
    class DemoSelectionViewModel : INotifyPropertyChanged
    {
        public void MethodBindingDemo()
        {
            Child = new MethodBindingViewModel();
        }
    }

View: 

    <Button Command="{Binding MethodBindingDemo}">
        <TextBlock TextWrapping="Wrap" Text="Bind to method demo"/>
    </Button>

Password Binding
---

Bind PasswordBox.Password to a secure string

View Model:

    class PasswordViewModel 
    {
        public SecureString Password { get; set; }
    }

View: 

    <PasswordBox 
	     monkey:PasswordBinding.IsPasswordBindingEnabled="True" 
		 monkey:PasswordBinding.Password="{Binding Password}"/>


Enum Binding
---

Using an enum with a radio button group is extreamly annoying and error-prone if you use view model bool properties.

Luckly, with this converter you can bind multiple radio buttons to the same enum property

    <UserControl.Resources>
        <monkey:EnumValueIsCheckedConverter x:Key="EnumRadioConverter"/>
    </UserControl.Resources>

	<RadioButton GroupName="MyGrp" IsChecked="{Binding TheValue, Converter={StaticResource EnumRadioConverter}, ConverterParameter={x:Static model:AnEnum.First}}">First</RadioButton>
	<RadioButton GroupName="MyGrp" IsChecked="{Binding TheValue, Converter={StaticResource EnumRadioConverter}, ConverterParameter={x:Static model:AnEnum.Second}}">Second</RadioButton>
	<RadioButton GroupName="MyGrp" IsChecked="{Binding TheValue, Converter={StaticResource EnumRadioConverter}, ConverterParameter={x:Static model:AnEnum.Third}}">Third</RadioButton>


View Locator
---

The 'ViewLocator' control will automatically find the view for your view model
without any mapping (if your view model ends with "ViewModel" your view has the same name but ends with "View"
and they are both in the same assembly)

Also, if you set the ViewLocator.HasViews attached property on and ItemsControl (ListBox, etc.) it will add a data template
with a ViewLocator so you can just bind a list of view models to ItemSource and the views will show up inside the list

View Model:

    [TypeDescriptionProvider(typeof(MethodBinding))]
    class DemoSelectionViewModel : INotifyPropertyChanged
    {
        public void MethodBindingDemo()
        {
            Child = new MethodBindingViewModel();
        }
    }

View: 

	<mvvm:ViewLocator DataContext="{Binding Child}"/>

Notify Property Changed
---

I don't know a way to get rid of all the boiler plate of INotifyPropertyChanged but I can 
write the OnPropertyChanged method - and, in the spirit of MvvmMonkey, you don't need to 
change you class heirarcy to use it

    private object _child;
    public object Child
    {
        get { return _child; }
        set
        {
            if(_child!=value)
            {
                _child = value;
                PropertyChange.Notify(this, PropertyChanged);
            }
        }
    }


Window Manager
---

One of the most anoying thing bout MVVM is that there isn't really a good way to open and close windows.

Either you break the View-View-Model seperation, hurt the system testebility or use an overkill messaging system.

The WindowManager addresses all of that, it has an interface like a simple light-weight system that doesn't really support testing 
but has a replacable implementation so it cn suport anything.

By default WindowManager uses ViewLocator to show the view inside the window

To open a view model in another window:

   WindowManager.OpenDialog(new WindowManagerChildViewModel());                

or

   WindowManager.OpenNonModal(new WindowManagerChildViewModel());

To close:

   WindowManager.Close(this);

To replace the windows manager code for unit testing or diffrent windowing strategy:

   WindowManager.SetImplementation(myWindowManager);


Licensing
---

MvvmMonkey is copyrighted by Nir Dobovizki, it uses the MIT license.

If you use this library I would love to know about it (but you are not required to tell me), also for any questions or suggestions you can contact me at nir@nbdtech.com