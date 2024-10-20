import grpc
from concurrent import futures
from Advisor.ai_advisor_pb2 import GetFinancialAdviceForAccountRequest, FinancialAdviceResponse
from Advisor.ai_advisor_pb2_grpc import AiAdvisorServicer, add_AiAdvisorServicer_to_server
from Advisor.FinancialAdvisorClass import FinancialAdvisor


class AiAdvisorService(AiAdvisorServicer):
    def GetFinancialAdvice(self, request: GetFinancialAdviceForAccountRequest, context):
        # Example: Use FinancialAdvisor to generate advice based on user data
        balance: float = request.balance
        goals: list[(str, float, float)] = [(i.category, i.amount_saved, i.total) for i in request.goals]
        transactions: list[(float, str)] = [(i.amount, i.category) for i in request.transactions]

        advisor = FinancialAdvisor(balance, transactions, goals)
        response = FinancialAdviceResponse()
        response.message = advisor.provide_financial_advice()
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
