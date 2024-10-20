from Chatbot import *
from nltk.stem import WordNetLemmatizer, PorterStemmer
from tensorflow.keras.models import load_model
from concurrent.futures import ThreadPoolExecutor
import logging
import json
import pickle
logging.basicConfig(level=logging.ERROR)

executor = ThreadPoolExecutor()
lemmatizer = WordNetLemmatizer()
stemmer = PorterStemmer()
with open('banking_intents.json', encoding='utf-8') as file:
    intents = json.load(file)
with open('vocabulary.pkl', 'rb') as vocab_file:
    vocabulary = pickle.load(vocab_file)

model = load_model('model.h5')
training_data = pickle.load(open('training_data.pkl', 'rb'))
words = training_data['words']
classes = training_data['classes']

while __name__ == '__main__':
    chatbot = ChatBot(executor, lemmatizer, stemmer, intents, vocabulary, model, training_data, words, classes)
    question = input()  # Prompt for user input
    response = chatbot.chat(question)
    print(response)