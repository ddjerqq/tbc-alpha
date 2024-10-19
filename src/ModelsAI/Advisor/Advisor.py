import grpc
from concurrent import futures
import FinancialAdvisor
import FinancialAdvisor_grpc

class AiAdvisorService(FinancialAdvisor_grpc.AiAdvisorServicer):
    def GetFinancialAdvice(self, request):
        # Here, implement the logic to generate financial advice
        # You have access to UserData with goals and transactions data

        # Example: Constructing FinancialAdvice response
        advisor = FinancialAdvisor(request.balance, request.transaction_data, request.goals_data)
        response = advisor.provide_financial_advice()
        return response


def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    FinancialAdvisor_grpc.add_AiAdvisorServicer_to_server(AiAdvisorService(), server)

    print("Server starting on port 50052...")
    server.add_insecure_port('[::]:50052')
    server.start()
    server.wait_for_termination()


if __name__ == "__main__":
    serve()
