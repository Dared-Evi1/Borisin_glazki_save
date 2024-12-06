﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Borisin_glazki_save
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        int countRecords;
        int countPage;
        int currentPage = 0;
        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;

        public ServicePage()
        {
            InitializeComponent();
            var currentAgents = Борисин_глазки_saveEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentAgents;
            filtr.SelectedIndex = 0;
            sort.SelectedIndex = 0;
            Update();
        }
        private void Update()
        {
            var currentAgents = Борисин_глазки_saveEntities.GetContext().Agent.ToList();


            if (filtr.SelectedIndex == 1)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 1).ToList();
            if (filtr.SelectedIndex == 2)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 2).ToList();
            if (filtr.SelectedIndex == 3)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 3).ToList();
            if (filtr.SelectedIndex == 4)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 4).ToList();
            if (filtr.SelectedIndex == 5)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 5).ToList();
            if (filtr.SelectedIndex == 6)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 6).ToList();

            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(Search.Text.ToLower())
                          || p.Email.ToLower().Contains(Search.Text.ToLower())
                          || p.Phone.ToLower().Contains(Search.Text.ToLower())).ToList();

            AgentListView.ItemsSource = currentAgents.ToList();

            if (sort.SelectedIndex == 1)
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Title).ToList();
            if (sort.SelectedIndex == 2)
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Title).ToList();
            if (sort.SelectedIndex == 3)
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Title).ToList();
            if (sort.SelectedIndex == 4)
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Title).ToList();
            if (sort.SelectedIndex == 5)
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Priority).ToList();
            if (sort.SelectedIndex == 6)
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Priority).ToList();
            AgentListView.ItemsSource = currentAgents;
            TableList = currentAgents;
            Change(0, 0);

        }

        private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Change(int napravlenie,int? selectedPage )
        {
            CurrentPageList.Clear();
            countRecords = TableList.Count;
            if (countRecords % 10 > 0)
            {
                countPage = countRecords / 10+1;
            }
            else
            {
                countPage = countRecords / 10;
            }
            Boolean Ifupdate = true;
            int min;
            if (selectedPage.HasValue)
            {
                if(selectedPage>=0 && selectedPage < countPage)
                {
                    currentPage = (int)selectedPage;
                    min = currentPage * 10 + 10 < countRecords ? currentPage * 10 + 10 : countRecords;
                    for(int i = currentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (napravlenie)
                {
                    case 1:
                        if (currentPage > 0)
                        {
                            currentPage--;
                            min = currentPage * 10 + 10 < countRecords ? currentPage * 10 + 10 : countRecords;
for(int i= currentPage * 10;i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;

                        case 2:
                        if (currentPage <countPage-1)
                        {
                            currentPage++;
                            min = currentPage * 10 + 10 < countRecords ? currentPage * 10 + 10 : countRecords;
                            for (int i = currentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for(int i = 1; i <= countPage; i++) {
                    PageListBox.Items.Add(i);
                        }
                PageListBox.SelectedIndex = currentPage;
                min = currentPage * 10 + 10 < countRecords ? currentPage * 10 + 10 : countRecords;
               Count.Text = min.ToString();
                Allrecords.Text = " из " +countRecords.ToString();
                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();
            }
        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Change(0,Convert.ToInt32(PageListBox.SelectedItem.ToString())-1);
        }

        private void left_Click(object sender, RoutedEventArgs e)
        {
            Change(1, null);
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            Change(2, null);

        }
    }
}