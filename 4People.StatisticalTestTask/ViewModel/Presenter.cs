using _4People.StatisticalTestTask.Model;
using _4People.StatisticalTestTask.Model.Strategy;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _4People.StatisticalTestTask.ViewModel
{
    public class Presenter : TargetToObserve
    {
        private readonly List<OutComesModel> _ensemble = new List<OutComesModel>();
        private int _NNumber;
        private string _inputfilepath;
        private int _hosts, _guests;
        private double _phost, _pguest, _pdeadheat, _pfreeze;

     
        public int CurrentGuestScore
        {
            get
            {
                return _guests;
            }
            set
            {
                _guests = value;
                RaisePropertyChangedEvent(nameof(CurrentGuestScore));
            }
        }
        public int CurrentHostScore
        {
            get
            {
                return _hosts;
            }
            set
            {
                _hosts = value;
                RaisePropertyChangedEvent(nameof(CurrentHostScore));
            }
        }

        public string InputFilePath
        {
            get
            {
                return _inputfilepath;
            }
            set
            {
                _inputfilepath = value;
                RaisePropertyChangedEvent(nameof(InputFilePath));
            }
        }


        /// <summary>
        /// Probabilities
        /// </summary>
        public double PHosts
        {
            get
            {
                return _phost;
            }
            set
            {
                _phost = value;
                RaisePropertyChangedEvent(nameof(PHosts));
                RaisePropertyChangedEvent(nameof(Sum));
            }
        }

        public double PGuests
        {
            get
            {
                return _pguest;
            }
            set
            {
                _pguest = value;
                RaisePropertyChangedEvent(nameof(PGuests));
                RaisePropertyChangedEvent(nameof(Sum));
            }
        }
        public double PDeadHeat
        {
            get
            {
                return _pdeadheat;
            }
            set
            {
                _pdeadheat = value;
                RaisePropertyChangedEvent(nameof(PDeadHeat));
                RaisePropertyChangedEvent(nameof(Sum));
            }
        }
        public double PFreeze
        {
            get
            {
                return _pfreeze;
            }
            set
            {
                _pfreeze = value;
                RaisePropertyChangedEvent(nameof(PFreeze));
                RaisePropertyChangedEvent(nameof(Sum));
            }
        }

        public double Sum =>  PDeadHeat + PGuests + PHosts;

         
        public int Number
        {
            get
            {
                return _NNumber;
            }
            set
            {
                _NNumber = value;
                RaisePropertyChangedEvent(nameof(Number));
            }

        }
        public ICommand EstimateCommand
        {
            get { return new DelegateCommand(Estimate); }
        }
        public ICommand BrowseDataCommand
        {
            get { return new DelegateCommand(BrowseData); }
        }
        public List<int> NumberOutcomesInterval
        {
            get
            {
                return new List<int> {1, 2, 3, 4, 5, 6 };
            }
        }
        public List<int> GoalsInterval
        {
            get {
                return new List<int> { 0,1,2,3,4,5,6};
            }
        }

        List<string> exceptions = new List<string>();

        private double SetField(IMethod method,int  Number, int CurrentGuestScore, int CurrentHostScore)
        {
            try
            {
                var estimate = new StatisticalEnsemble(method, _ensemble, Number, CurrentGuestScore, CurrentHostScore);
                return estimate.Estimate(Number, CurrentGuestScore, CurrentHostScore);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                exceptions.Add(ex.Message);
                return 0.0;
            }
        }
    


        private void Estimate()
        {
            //if (Upper >= Lower)
            //{
            exceptions.Clear();
            if (_ensemble.Count > 0)
            {
                if (Number > 0)
                {
                    PHosts = SetField(new HostsVictory(), Number, CurrentGuestScore, CurrentHostScore);
                    PGuests = SetField(new GuestsVictory(), Number, CurrentGuestScore, CurrentHostScore);
                    PDeadHeat = SetField(new DeadHeat(), Number, CurrentGuestScore, CurrentHostScore);
                    PFreeze = SetField(new Freeze(), Number, CurrentGuestScore, CurrentHostScore);

                    if (exceptions.Count > 0)
                    {
                        MessageBox.Show(string.Join(Environment.NewLine, exceptions), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    //    var estimatorDeadHeat = new StatisticalEnsemble(new DeadHeat(), _ensemble, Number, CurrentGuestScore, CurrentHostScore);
                    //    var estimatorGuest = new StatisticalEnsemble(new GuestsVictory(), _ensemble, Number, CurrentGuestScore, CurrentHostScore);
                    //    var estimatorHosts = new StatisticalEnsemble(new HostsVictory(), _ensemble, Number, CurrentGuestScore, CurrentHostScore);
                    //    var estimatorStuck = new StatisticalEnsemble(new Stuck(), _ensemble, Number, CurrentGuestScore, CurrentHostScore);
                    //try
                    //{
                    //    PHosts = estimatorHosts.Estimate(Number, CurrentGuestScore, CurrentHostScore);
                    //    PGuests = estimatorGuest.Estimate(Number, CurrentGuestScore, CurrentHostScore);
                    //    PDeadHeat = estimatorDeadHeat.Estimate(Number, CurrentGuestScore, CurrentHostScore);
                    //    PFreeze = estimatorStuck.Estimate(Number, CurrentGuestScore, CurrentHostScore);
                    //}
                    //catch (Exception ex)
                    //{

                    //    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}


                }
                else
                {
                    MessageBox.Show("Incorrect interval's boundaries", Constants.Caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else {
                MessageBox.Show("The statistical ensemble is empty!", Constants.Caption, MessageBoxButton.OK, MessageBoxImage.Warning);

            }        
        }
        /// <summary>
        /// look for a matrix
        /// </summary>
        private void BrowseData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                InputFilePath = openFileDialog.FileName;
                Calculate();


            }
        }



        private void Calculate()
        {

            _ensemble.Clear();
            try
            {
                using (StreamReader sr = new StreamReader(InputFilePath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        _ensemble.Add(new OutComesModel(line));
                    }

                    
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


        }


    }
}
