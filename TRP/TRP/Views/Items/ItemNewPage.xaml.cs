﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TRP.Controllers;
using TRP.Models;

namespace TRP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemNewPage : ContentPage
    {
        public Item Data { get; set; } // Data for this page

        // Constructor for the page, will create a new blank item that can tehn get updated
        public ItemNewPage()
        {
            InitializeComponent();

            Data = new Item
            {
                Name = "",
                Description = "",
                Id = Guid.NewGuid().ToString(),
                Range=0,
                Value=0,
                ImageURI = ItemsController.DefaultImageURI
            };

            BindingContext = this;
            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = Data.Location.ToString();
            AttributePicker.SelectedItem = Data.Attribute.ToString();
        }

        // Respond to the Save click
        // Send the add message to so it gets added...
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = ItemsController.DefaultImageURI;
            }

            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // The stepper function for Range
        void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeValue.Text = String.Format("{0}", e.NewValue);
        }

        // The stepper function for Value
        void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueValue.Text = String.Format("{0}", e.NewValue);
        }

        // The stepper function for Damage
        void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageValue.Text = String.Format("{0}", e.NewValue);
        }

        // Toggles consumable flag
        private void Consumable_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                Data.Consumable = true;
            }
            else
            {
                Data.Consumable = false;
            }

        }

    }
}