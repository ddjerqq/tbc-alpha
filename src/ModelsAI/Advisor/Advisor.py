import grpc
from concurrent import futures
from ai_advisor_pb2 import GetFinancialAdviceForAccountRequest, FinancialAdviceResponse
from ai_advisor_pb2_grpc import AiAdvisorServicer, add_AiAdvisorServicer_to_server
from src.ModelsAI.Advisor.FinancialAdvisorClass import FinancialAdvisor


class AiAdvisorService(AiAdvisorServicer):
    def GetFinancialAdvice(self, request: AiAdvisorServicer, context):
        # advisor = FinancialAdvisor(request.balance, request.transactions, request.goals)
        # response = advisor.provide_financial_advice()

        response = FinancialAdviceResponse()
        response.message = "hello"
        return response


def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    add_AiAdvisorServicer_to_server(AiAdvisorService(), server)

    print("Server starting on port 5001...")
    server.add_insecure_port('localhost:5001')
    server.start()
    server.wait_for_termination()


if __name__ == "__main__":
    serve()
