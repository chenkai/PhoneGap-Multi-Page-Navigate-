using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using SandwichFlow.Util;


namespace SandwichFlow
{
  public partial class MainPage : PhoneApplicationPage
  {
    private WebBrowserHelper _browserHelper;

    // Constructor
    public MainPage()
    {
      InitializeComponent();

      new BackButtonHandler(this, PGView);
      _browserHelper = new WebBrowserHelper(PGView.Browser);

      PGView.Browser.ScriptNotify += Browser_ScriptNotify;
      PGView.Browser.Navigated += Browser_Navigated;

      EventHandler<NavigationEventArgs> hideSplashScreen = null;
      hideSplashScreen = (s, e ) =>
        {
          fadeOut.Begin();
          PGView.Browser.Navigated -= hideSplashScreen;
        };
      PGView.Browser.Navigated += hideSplashScreen;
    }

    private void Browser_Navigated(object sender, NavigationEventArgs e)
    {
      // when we first navigate to a page, we assume that it can be scrolled
      _browserHelper.ScrollDisabled = false;
    }

    private void Browser_ScriptNotify(object sender, NotifyEventArgs e)
    {
      // if a page notifies that it should not be scrollable, disable
      // scrolling.
      if (e.Value == "noScroll")
      {
        _browserHelper.ScrollDisabled = true;
      }
    }

    private void fadeOut_Completed(object sender, EventArgs e)
    {
      splashImage.Visibility = Visibility.Collapsed;
    }

  }
}