import grpc
from concurrent import futures
from ai_advisor_pb2 import GetFinancialAdviceForAccountRequest, FinancialAdviceResponse
from ai_advisor_pb2_grpc import AiAdvisorServicer, add_AiAdvisorServicer_to_server
from src.ModelsAI.Advisor.FinancialAdvisorClass import FinancialAdvisor


class AiAdvisorService(AiAdvisorServicer):
    def GetFinancialAdvice(self, request: GetFinancialAdviceForAccountRequest, context):
        # Example: Use FinancialAdvisor to generate advice based on user data
        # advisor = FinancialAdvisor(request.balance, request.transactions, request.goals)
        # response_message = advisor.provide_financial_advice()

        balance: float = request.balance
        goals: list[(str, float, float)] = [(i.category, i.amount_saved, i.total) for i in request.goals]
        transactions: list[(str, float)] = [(i.category, i.amount) for i in request.transactions]

        # Create a FinancialAdviceResponse and populate it with the advice
        response = FinancialAdviceResponse()
        response.message = "hello"  # Example static message; replace with dynamic response logic
        return response


def serve():
    # Create the gRPC server
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))

    # Add the AiAdvisorService to the server
    add_AiAdvisorServicer_to_server(AiAdvisorService(), server)

    # Start the server on port 5001
    print("Server starting on port 5001...")
    server.add_insecure_port('[::]:5001')  # Using [::] ensures compatibility with both IPv4 and IPv6
    server.start()

    # Keep the server running
    server.wait_for_termination()


if __name__ == "__main__":
    serve()
