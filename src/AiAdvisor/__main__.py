import grpc
from concurrent import futures
import time

# Import the generated classes
import Greet_pb2
import Greet_pb2_grpc

# Implement the GreetService from the proto file
class GreetService(Greet_pb2_grpc.GreetServiceServicer):
    def Greet(self, request, context):
        response = Greet_pb2.GreetResponse()
        response.greeting = f"Hello, {request.username}!"
        return response

# Create the gRPC server
def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    Greet_pb2_grpc.add_GreetServiceServicer_to_server(GreetService(), server)

    print("Server starting on port 50051...")
    server.add_insecure_port('[::]:50051')
    server.start()

if __name__ == "__main__":
    serve()