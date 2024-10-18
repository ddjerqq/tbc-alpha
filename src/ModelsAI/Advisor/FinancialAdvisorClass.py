from libraries import np, pd


class FinancialAdvisor:
    def __init__(self):  # , user_data, account_data, transaction_data, goals_data):
        # self.user_data = user_data
        # self.account_data = account_data
        # self.transaction_data = transaction_data
        # self.goals_data = goals_data
        pass

    def spending_control(self) -> str or None:
        pass

    def spending_categories(self) -> str or None:
        pass

    def emergency_fund(self) -> str or None:
        pass

    def savings_rate(self) -> str or None:
        pass

    def goal_progress(self) -> str or None:
        pass

    def provide_financial_advice(self) -> str:
        advice = [self.spending_control(),
                  self.spending_categories(),
                  self.emergency_fund(),
                  self.savings_rate(),
                  self.goal_progress()]
        try:
            final_advice = "\n".join([f"{i + 1}. {item}" for i, item in enumerate(advice)])
        except TypeError:
            final_advice = "No advice available at this time."

        return final_advice
