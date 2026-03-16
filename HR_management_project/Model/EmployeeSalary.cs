using System;
using System.Collections.Generic;
using System.Text;

namespace HR_management_project.Model
{
    public class EmployeeSalary
    {
        public decimal BaseSalary { get; private set; }
        public decimal Deduction { get; private set; }
        public decimal Bonus { get; private set; }
        public decimal NetSalary => BaseSalary - Deduction + Bonus;

        public EmployeeSalary() { }
       
        public EmployeeSalary(decimal baseSalary, decimal deduction, decimal bonus)
        {
            SetBaseSalary(baseSalary);
            SetBonus(bonus);
            SetDeduction(deduction);
        }
        public EmployeeSalary(decimal baseSalary)
        {
            SetBaseSalary(baseSalary);
            Deduction = 0;
            Bonus = 0;
        }
        // make it more flixible with a delegete
        private void SetBaseSalary(decimal baseSalary)
        {
            if (baseSalary < 100)
                throw new ArgumentException("Base salary must be at least 100.");

            BaseSalary = baseSalary;
        }
        public void SetDeduction(decimal deduction)
        {
            if (deduction < 0)
                throw new ArgumentException("Deduction cannot be negative.");
            Deduction = deduction;
        }
        public void SetBonus(decimal bonus)
        {
            if (bonus < 0)
                throw new ArgumentException("Bonus cannot be negative.");
            Bonus = bonus;
        }

        
    }
}
