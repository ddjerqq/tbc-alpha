from libraries import *


class FinancialAdvisor:
    def __init__(self, user_data, account_data, transaction_data, goals_data):
        self.user_data = user_data
        self.account_data = account_data
        self.transaction_data = transaction_data
        self.goals_data = goals_data

    def provide_financial_advice(self, user_id):
