## Navigation

In Part 3 we will add simple navigation to push a new page onto the stack to display details about the monkey.

### Add Selected Event

Now, let's add navigation to a second page that displays monkey details!

1. In `MainPage.xaml` we can add an `ItemSelected` event to the `ListView`:

Before:

```xml
<ListView ItemsSource="{Binding Monkeys}"
            HasUnevenRows="True"
            Grid.ColumnSpan="2">
```

After:
```xml
<ListView ItemsSource="{Binding Monkeys}"
            ItemSelected="ListView_ItemSelected"
            HasUnevenRows="True"
            Grid.ColumnSpan="2">
```


2. In `MainPage.xaml.cs`, create a method called `ListView_ItemSelected`:
    - This code checks to see if the selected item is non-null and then use the built in `Navigation` API to push a new page and deselect the item.

```csharp
async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
{
    var monkey = e.SelectedItem as Monkey;
    if (monkey == null)
        return;

    await Navigation.PushAsync(new DetailsPage(monkey));

    ((ListView)sender).SelectedItem = null;
}
```

### ViewModel for Details

1. Inside of our `ViewModel/MonkeyDetailsViewModel.cs` will house our logic for assigning the monkey to the view model.

Let's first create a bindable property for the `Monkey`:

```csharp
public class MonkeyDetailsViewModel : BaseViewModel
{
    public MonkeyDetailsViewModel()
    {
    }

    public MonkeyDetailsViewModel(Monkey monkey)
        : this()
    {
        Monkey = monkey;
        Title = $"{Monkey.Name} Details";
    }
    Monkey monkey;
    public Monkey Monkey
    {
        get => monkey;
        set
        {
            if (monkey == value)
                return;

            monkey = value;
            OnPropertyChanged();
        }
    }
}
```

### Create DetailsPage.xaml UI

Let's add UI to the DetailsPage. Our end goal is to get a fancy profile screen like this:

![](Art/Details.PNG)

At the core is a `ScrollView`, `StackLayout`, and `Grid` to layout all of the controls nicely on the screen:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage 
             Title="{Binding Title}"> <!-- Add this-->
    <d:ContentPage.BindingContext>
        <viewmodel:MonkeyDetailsViewModel/>
    </d:ContentPage.BindingContext>
    <ScrollView>
        <StackLayout>
            <Grid>
                <!-- Monkey image and background -->
            </Grid>   
            <!-- Name and details -->
        </StackLayout>
    </ScrollView>
</ContentPage>
```

We can now fill in our `Grid` with the following code:

```xml
<Grid.RowDefinitions>
    <RowDefinition Height="100"/>
    <RowDefinition Height="Auto"/>
</Grid.RowDefinitions>
<Grid.ColumnDefinitions>
    <ColumnDefinition Width="*"/>
    <ColumnDefinition Width="Auto"/>
    <ColumnDefinition Width="*"/>
</Grid.ColumnDefinitions>
<BoxView BackgroundColor="{StaticResource Primary}" HorizontalOptions="FillAndExpand"
            HeightRequest="100" Grid.ColumnSpan="3"/>
<StackLayout Grid.RowSpan="2" Grid.Column="1" Margin="0,50,0,0">

    <imagecircle:CircleImage FillColor="White" 
                            BorderColor="White"
                            BorderThickness="2"
                            Source="{Binding Monkey.Image}"
                            VerticalOptions="Center"
                            HeightRequest="100"
                            WidthRequest="100"
                            Aspect="AspectFill"/>
</StackLayout>

<Label FontSize="Micro" Text="{Binding Monkey.Location}" HorizontalOptions="Center" Grid.Row="1" Margin="10"/>
<Label FontSize="Micro" Text="{Binding Monkey.Population}" HorizontalOptions="Center" Grid.Row="1" Grid.Column="2" Margin="10"/>
```

Finally, under the `Grid`, but inside of the `StackLayout` we will add details about the monkey.

```xml
<Label Text="{Binding Monkey.Name}" HorizontalOptions="Center" FontSize="Medium" FontAttributes="Bold"/>

<BoxView HeightRequest="1" Color="#DDDDDD"/>

<Label Text="{Binding Monkey.Details}" Margin="10"/>
```
