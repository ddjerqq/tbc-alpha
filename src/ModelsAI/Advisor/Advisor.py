import grpc
from concurrent import futures
import ai_advisor_pb2
from ai_advisor_pb2_grpc import AiAdvisorServicer, add_AiAdvisorServicer_to_server
from src.ModelsAI.Advisor.FinancialAdvisorClass import FinancialAdvisor


class AiAdvisorService(AiAdvisorServicer):
    def GetFinancialAdvice(self, request, context):
        # Here, implement the logic to generate financial advice
        # You have access to UserData with goals and transactions data

        # Example: Constructing FinancialAdvice response
        advisor = FinancialAdvisor(request.balance, request.transaction_data, request.goals_data)
        response = advisor.provide_financial_advice()
        return response


def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    add_AiAdvisorServicer_to_server(AiAdvisorService(), server)

    print("Server starting on port 50052...")
    server.add_insecure_port('[::]:50052')
    server.start()
    server.wait_for_termination()


if __name__ == "__main__":
    serve()
