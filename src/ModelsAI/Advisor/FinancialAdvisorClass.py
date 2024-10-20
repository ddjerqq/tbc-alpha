class FinancialAdvisor:
    def __init__(self, balance: float, transaction_data: list[(str, float)], goals_data: list[(str, float, float)]):
        self.balance = balance
        self.income = 0
        self.spending = 0
        self.spent_unnecessary = []
        self.spending_on_needs = 0
        self.current_fund = 0
        self.amount_saved = 0
        self.total = 1

        for i in range(len(transaction_data)):
            if transaction_data[i][0] > 0:
                self.income += round(transaction_data[i][0], 2)
            else:
                self.spending += abs(round(transaction_data[i][0], 2))

            if transaction_data[i][1] in ['Groceries', 'Rent', 'Utilities']:
                self.spending_on_needs += abs(round(transaction_data[i][0], 2))
            elif not transaction_data[i][1] == 'Salary':
                self.spent_unnecessary.append(transaction_data[i])

        if not len(goals_data) == 0:
            for i in range(len(goals_data)):
                if goals_data[i][0] == 'emergency_fund':
                    self.current_fund += round(goals_data[i][1], 2)

                self.amount_saved += round(goals_data[i][1], 2)
                self.total += round(goals_data[i][2], 2)

        self.goal_progress = round(self.amount_saved / self.total, 2)
        self.advice = []

    def spending_control(self, income, spending, spending_on_needs) -> None:
        if spending != 0 and income != 0:
            needs, wants = round(income * 0.5), round(income * 0.3)
            if spending_on_needs < needs * 0.7 and spending_on_needs / spending < 0.3:
                self.advice.append(
                    f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} "
                    f"of your income and {spending_on_needs / spending:.2%} of your total monthly spending. "
                    f"This means you are spending more than 70% of your total spending is in categories that are not very important.")
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
                    f"This means you are spending too much in total and more than 70% of your total spending is in categories that is not very important. "
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
                    f"This means you are spending too much in needs but your needs/wants/saving ratio is generally well.")
            else:
                self.advice.append(
                    f"You've spent ${spending_on_needs:.0f} in your needs, which is {spending_on_needs / income:.2%} of your income "
                    f"and {spending_on_needs / spending:.2%} of your total monthly spending. This means your spending pattern is generally well.")

            if spending < income * 0.5:
                self.advice.append(
                    f"Great job! Your spending is very well under control at {spending / income:.2%} of your income. Keep it up!")
            elif 0.5 <= spending < 0.8:
                self.advice.append(
                    f"Your spending is moderate at {spending / income:.2%} of your income. Consider reducing discretionary expenses to save more.")
            else:
                self.advice.append(
                    f"Your spending is too high at {spending / income:.2%} of your income. It's time to reevaluate and cut down on non-essential costs.")

    def spending_categories(self, spent_unnecessary, spending) -> None:
        spent_on_entertainment = 0
        spent_on_dining = 0
        spent_on_shopping = 0

        for i in range(len(spent_unnecessary)):
            print(len(spent_unnecessary))
            if spent_unnecessary[i][1] == 'Entertainment':
                spent_on_entertainment += abs(spent_unnecessary[i][0])
            elif spent_unnecessary[i][1] == 'Dining':
                spent_on_dining += abs(spent_unnecessary[i][0])
            else:
                spent_on_shopping += abs(spent_unnecessary[i][0])

        if spending != 0:
            print(spent_on_entertainment, spent_on_dining, spent_on_shopping)
            if spent_on_entertainment > spending * 0.15:
                self.advice.append(
                    f"You are spending too much on entertainment ({spent_on_entertainment / spending:.2%} of your monthly spending). "
                    f"Try reducing it to 10-15%.")
            if spent_on_dining > spending * 0.1:
                self.advice.append(f"You are spending {spent_on_dining / spending:.2%} of your monthly spending on dining. "
                                   f"Consider cooking at home more often to save money.")
            if spent_on_shopping > spending * 0.2:
                self.advice.append(f"You're spending a significant amount ({spent_on_shopping / spending:.2%}) on shopping. "
                                   f"Try to set a monthly budget to manage this better.")

    def emergency_fund(self, current_fund, spending) -> None:
        emergency_fund = round(spending * 3, 2)
        if current_fund == 0:
            self.advice.append(f"You have no emergency fund. consider creating one to improve your financial security.")
        elif current_fund < emergency_fund:
            shortfall = emergency_fund - current_fund
            self.advice.append(
                f"You have ${current_fund} saved so far in your emergency fund. To complete it, aim to save an additional ${shortfall:.0f}.")
        else:
            self.advice.append(
                f"Congratulations! You've reached your emergency fund goal of ${emergency_fund:.0f} and have ${current_fund:.0f} in your emergency fund")

    def savings_rate(self, income, spending) -> None:
        if not income < 0:
            savings_rate = round((income - spending) / income, 2)
            if savings_rate > 0.3:
                self.advice.append(
                    f"You're saving {savings_rate:.2%} of your income, which is excellent! Consider transferring a portion to your saving accounts.")
            elif 0.1 <= savings_rate <= 0.3:
                self.advice.append(
                    f"You're saving {savings_rate:.2%} of your income. That's a good start, but you might want to increase your savings to prepare for long-term goals.")
            elif savings_rate <= 0:
                self.advice.append(
                    f"You're saving 0% of your income. Try to increase your savings to at least 10% by reducing non-essential spending.")
            else:
                self.advice.append(
                    f"You're only saving {savings_rate:.2%} of your income. Try to increase your savings to at least 10% by reducing non-essential spending.")

    def progress(self, goal_progress) -> None:
        if not goal_progress == 0:
            if goal_progress < 0.5:
                self.advice.append(
                    f"You're only {goal_progress:.2%} of the way toward your all saving goals. Stay focused and try to increase your monthly contributions.")
            elif 0.5 <= goal_progress < 1:
                self.advice.append(
                    f"You're making great progress! You've reached {goal_progress:.2%} of your goals. Keep going, you're almost there.")
            else:
                self.advice.append(
                    f"Congratulations! You've achieved your savings goals. Consider setting a new goal to continue growing your wealth.")
        else:
            self.advice.append("You have no saving goals. consider creating some important ones to achieve your wishes")

    def provide_financial_advice(self) -> str:
        self.spending_control(self.income, self.spending, self.spending_on_needs)
        self.spending_categories(self.spent_unnecessary, self.spending)
        self.emergency_fund(self.current_fund, self.spending)
        self.savings_rate(self.income, self.spending)
        self.progress(self.goal_progress)
        try:
            return "\n".join([f"{i + 1}. {item}" for i, item in enumerate(self.advice)])
        except Exception as e:
            print(e)
            return "No advice available at this time."
