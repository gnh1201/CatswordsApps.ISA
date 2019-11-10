using CatswordsApps.ISA.Model;
using CatswordsApps.ISA.Service;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace CatswordsApps.ISA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public class _NoticeModel {
            public DateTime CreatedOn { get; set; }
            public string Content { get; set; }
        }

        public class _BundleModel
        {
            public string InstallDate { get; set; }
            public string DisplayName { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            // Assign
            try
            {
                string filename = AppDataService.GetFilePath("ISA.AssignNumber.txt");
                string assignNumber = File.ReadAllText(filename);
                tbAssignNumber.Text = assignNumber;

                AssignService.AssignNumber = tbAssignNumber.Text;
                AssignService.DataIn();
                List<AssignModel> assigns = AssignService.Data;
                foreach (AssignModel assign in assigns)
                {
                    tbAssignNumber.Text = assign.AssignNumber;
                    tbBizName.Text = assign.BizName;
                    tbUserContact.Text = assign.UserContact;
                    tbUserName.Text = assign.UserName;
                    tbUserOrganization.Text = assign.UserOrganization;
                }
            }
            catch(Exception)
            {
                // pass
            }

            // Devices
            try
            {
                List<DeviceModel> devices = new List<DeviceModel>();
                DeviceModel device = new DeviceModel
                {
                    IP = NetworkService.GetPrimaryIP(),
                    MAC = NetworkService.GetPrimaryMAC(),
                    Version = RegistryService.GetOSVersion(),
                    Name = RegistryService.GetComputerName()
                };
                devices.Add(device);
                DeviceService.DataOut(devices);

                tbDeviceIP.Text = device.IP;
                tbDeviceMAC.Text = device.MAC;
                tbDeviceVersion.Text = device.Version;
                tbDeviceName.Text = device.Name;
            }
            catch (Exception)
            {
                // pass
            }

            // Notices
            try {
                NoticeService.DataIn();
                List<_NoticeModel> _notices = new List<_NoticeModel>();
                List<NoticeModel> notices = NoticeService.Data;
                foreach(NoticeModel item in notices)
                {
                    _notices.Add(new _NoticeModel {
                        CreatedOn = item.CreatedOn,
                        Content = item.Content
                    });
                }
                dgNotice.ItemsSource = _notices;
            }
            catch (Exception)
            {
                // pass
            }

            // Bundles (Data Out)
            try
            {
                BundleService.DataOut(RegistryService.GetAllBundles());
            }
            catch (Exception)
            {
                // pass
            }

            // Bundles (Data In)
            try
            {
                BundleService.DataIn();
                List<_BundleModel> _bundles = new List<_BundleModel>();
                List<BundleModel> bundles = BundleService.Data;
                foreach (BundleModel item in bundles)
                {
                    _bundles.Add(new _BundleModel
                    {
                        InstallDate = (string.IsNullOrEmpty(item.InstallDate) ? "Unknown" : item.InstallDate),
                        DisplayName = string.Format("{0} ({1})", (string.IsNullOrEmpty(item.DisplayName) ? "Unknown" : item.DisplayName), item.Origin)
                    });
                }
                dgBundle.ItemsSource = _bundles;
            }
            catch (Exception)
            {
                // pass
            }
        }

        private void OnClick_btAssign(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbAssignNumber.Text))
            {
                MessageBox.Show("계약번호를 입력하여 주세요.");
            } else
            {
                string filename = AppDataService.GetFilePath("ISA.AssignNumber.txt");
                File.WriteAllText(filename, tbAssignNumber.Text);

                AssignService.AssignNumber = tbAssignNumber.Text;
                AssignService.DataIn();
                List<AssignModel> assigns = AssignService.Data;
                foreach(AssignModel assign in assigns)
                {
                    tbAssignNumber.Text = assign.AssignNumber;
                    tbBizName.Text = assign.BizName;
                    tbUserContact.Text = assign.UserContact;
                    tbUserName.Text = assign.UserName;
                    tbUserOrganization.Text = assign.UserOrganization;
                }
            }
        }
    }
}
