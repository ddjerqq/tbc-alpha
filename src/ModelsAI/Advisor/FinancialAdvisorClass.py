from libraries import np, pd


class FinancialAdvisor:
    def __init__(self, user_data, accounts_data, transaction_data, goals_data):
        self.user_data = user_data
        self.accounts_data = accounts_data
        self.transaction_data = transaction_data
        self.goals_data = goals_data
        self.advice = []

    def spending_control(self, income: float, spending: float, spending_on_needs: float) -> None:
        needs, wants = round(income * 0.5), round(income * 0.3)
        if spending_on_needs < needs * 0.7 and spending_on_needs / spending < 0.3:
            self.advice.append(
                f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} "
                f"of your income and {spending_on_needs / spending:.2%} of your total monthly spending. "
                f"This means you are spending more than 70% of your total spendings is in categories that are not very important.")
        elif spending_on_needs < needs * 0.7 and spending_on_needs / spending > 0.7:
            self.advice.append(
                f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} "
                f"of your income and {spending_on_needs / spending:.2%} of your total monthly spending. "
                f"This means you are saving most of your income which is very amazing "
                f"but take care of yourself and try to satisfy your needs.")
        elif spending_on_needs < needs * 0.7:
            self.advice.append(
                f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} "
                f"of your income and {spending_on_needs / spending:.2%} of your total monthly spending. "
                f"This means your spending pattern is normal and you are saving a part of your income which is good. "
                f"But consider spending less in categories that is not much important. "
                f"Try to take more care of yourself and satisfy your needs.")
        elif spending_on_needs > needs * 1.2 and spending_on_needs / spending < 0.3:
            self.advice.append(
                f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} of your income "
                f"and {spending_on_needs / spending:.2%} of your total monthly spending. "
                f"This means you are spending too much in total and more than 70% of your total spendings is in categories that is not very important. "
                f"Consider stabilizing your spending")
        elif spending_on_needs > needs * 1.2 and spending_on_needs / spending > 0.7:
            self.advice.append(
                f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} of your income "
                f"and {spending_on_needs / spending:.2%} of your total monthly spending. "
                f"This means you are spending too much in needs but you are spending less in unimportant categories which is so good.")
        elif spending_on_needs > needs * 1.2:
            self.advice.append(
                f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} "
                f"of your income and {spending_on_needs / spending:.2%} of your total monthly spending. "
                f"This means you are spending too much in needs but your needs/wants/saving ratio is generaly well.")
        else:
            self.advice.append(
                f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} of your income "
                f"and {spending_on_needs / spending:.2%} of your total monthly spending. This means your spending pattern is generaly well.")

        if spending < income * 0.5:
            self.advice.append(
                f"Great job! Your spending is very well under control at {spending / income:.2%} of your income. Keep it up!")
        elif 0.5 <= spending < 0.8:
            self.advice.append(
                f"Your spending is moderate at {spending / income:.2%} of your income. Consider reducing discretionary expenses to save more.")
        else:
            self.advice.append(
                f"Your spending is too high at {spending / income:.2%} of your income. It's time to reevaluate and cut down on non-essential costs.")

        return

    def spending_categories(self, transaction_data, spending) -> None:
        advice = []
        average_monthly_spending = transaction_data  # !!!needs to be completed!!!
        if 'Entertainment' in average_monthly_spending.index:
            entertainment_avg = average_monthly_spending['Entertainment']  # Monthly average entertainment spending
            if entertainment_avg > spending * 0.15:
                self.advice.append(
                    f"You are spending too much on entertainment ({entertainment_avg / spending:.2%} of your monthly spending). Try reducing it to 10-15%.")

            # Check for 'Dining' spending
        if 'Dining' in average_monthly_spending.index:
            dining_avg = average_monthly_spending['Dining']  # Monthly average dining spending
            if dining_avg > spending * 0.1:
                self.advice.append(
                    f"You are spending {dining_avg / spending:.2%} of your monthly spending on dining. Consider cooking at home more often to save money.")

            # Check for 'Shopping' spending
        if 'Shopping' in average_monthly_spending.index:
            shopping_avg = average_monthly_spending['Shopping']  # Monthly average shopping spending
            if shopping_avg > spending * 0.2:
                self.advice.append(
                    f"You're spending a significant amount ({shopping_avg / spending:.2%}) on shopping. Try to set a monthly budget to manage this better.")

    def emergency_fund(self):
        pass

    def savings_rate(self) -> None:
        pass

    def goal_progress(self) -> None:
        pass

    def provide_financial_advice(self) -> str:
        self.spending_control()
        self.spending_categories()
        self.emergency_fund()
        self.savings_rate()
        self.goal_progress()
        try:
            final_advice = "\n".join([f"{i + 1}. {item}" for i, item in enumerate(self.advice)])
        except TypeError:
            final_advice = "No advice available at this time."

        return final_advice
