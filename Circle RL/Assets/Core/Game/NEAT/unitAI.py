import os
import neat
import socket
import json
import queue
import threading


ACTIVATION_THRESHOLD = 0.5

config_hexagons = None
config_circles = None
config_squares = None
config_triangles = None
config_rectangles = None
config_player_agents = None

population_hexagons = None
population_circles = None
population_squares = None
population_triangles = None
population_rectangles = None
population_player_agents = None


class Enemy:
    def __init__(self, sensors: list, position: list,
                 health, target_position: list, is_alive,
                 max_score, current_score,
                 predicted_input: list, current_player_weapon: list):
        self.sensors = sensors
        self.position = position
        self.health = health
        self.is_alive = is_alive
        self.max_score = max_score
        self.current_score = current_score
        self.target_position = target_position
        self.predicted_input = predicted_input
        self.current_player_weapon = current_player_weapon


class PlayerAgent:
    def __init__(self,
                 sensors, position, health, target_position, is_alive,
                 max_score, current_score, current_weapon):
        self.sensors = sensors
        self.position = position
        self.health = health
        self.is_alive = is_alive
        self.max_score = max_score
        self.current_score = current_score
        self.target_position = target_position
        self.current_weapon = current_weapon


def get_unity_data():
    while True:
        data = client_socket.recv(4096 * 4).decode()
        # print(data)
        parsed_json = json.loads(data)

        if parsed_json["command"] == "Initialize algorithm":
            print("Error: algorithm is initialized")
            exit(1)
        elif parsed_json["command"] == "Create population":
            send_unity_outputs("out")
            print("Error: population is created")
            send_unity_outputs("out")
            exit(1)
        elif parsed_json["command"] == "Evaluate population":
            send_unity_outputs("out")
            return "new"
        elif parsed_json["command"] == "Process individuals data":
            return parsed_json["data"]
        else:
            print("Error: wrong command")
            exit(1)


def parse_unity_data(data, name):

    agents = []
    agents_data = data[name]

    if not agents_data:
        return agents

    for item in agents_data:
        if name == 'Player':
            agent = PlayerAgent()
            agent.current_weapon = item['CurrentWeapon']
        else:
            agent = Enemy()
            agent.predicted_input = item['PredictedInput']
            agent.current_player_weapon = item["CurrentPlayerWeapon"]
        agent.sensors = item['Sensors']
        agent.position = item['Position']
        agent.health = item['Health']
        agent.is_alive = item['IsAlive']
        agent.max_score = item['MaxScore']
        agent.current_score = item['CurrentScore']
        agent.target_position = item['TargetPosition']
        agents.append(agent)
    return agents


def send_unity_outputs(outputs):
    response = outputs.lower()
    client_socket.send(response.encode())


def check_agents_alive(agents):
    count = 0
    for agent in agents:
        if not(agent.is_alive):
            count += 1
    if count == len(agents):
        return False
    else:
        return True


def initialize_config(config_path):
    config = neat.config.Config(neat.DefaultGenome, neat.DefaultReproduction,
                                neat.DefaultSpeciesSet, neat.DefaultStagnation,
                                config_path)
    return config


def create_population(config):

    p = neat.Population(config)

    p.add_reporter(neat.StdOutReporter(True))
    stats = neat.StatisticsReporter()
    p.add_reporter(stats)

    return p


def eval_genomes_enemies(genomes, config):

    nets = []
    agents = []
    ge = []

    for genome_id, genome in genomes:
        genome.fitness = 0
        net = neat.nn.FeedForwardNetwork.create(genome, config)
        nets.append(net)
        ge.append(genome)

    run = True
    while run:
        data = get_unity_data()
        if(data == "new"):
            break
        agents = parse_unity_data(data, "enemy")

        unity_outputs = []

        for x, agent in enumerate(agents):
            if not(agent.is_alive):
                unity_outputs.append([False, False])
                continue

            ge[x].fitness = agent.current_score

            output = nets[agents.index(agent)].activate([
                agent.sensors[0],
                agent.sensors[1],
                agent.sensors[2],
                agent.sensors[3],
                agent.sensors[4],
                agent.sensors[5],
                agent.sensors[6],
                agent.sensors[7],
                agent.position[0],
                agent.position[1],
                agent.health,
                agent.target_position[0],
                agent.target_position[1],
                agent.predicted_input[0],
                agent.predicted_input[1],
                agent.predicted_input[2],
                agent.predicted_input[3],
                agent.predicted_input[4],
                agent.predicted_input[5],
                agent.predicted_input[6],
                agent.predicted_input[7],
                agent.current_player_weapon[0],
                agent.current_player_weapon[1],
                agent.current_player_weapon[2]])

            unity_output = []

            for i in range(3):
                if i == 2:
                    if output[i] > ACTIVATION_THRESHOLD:
                        unity_output.append(True)
                    else:
                        unity_output.append(False)
                else:
                    unity_output.append(output[i])

            unity_outputs.append(unity_output.copy())
        all_queues[0].put(unity_outputs)


def eval_genomes_player_agents(genomes, config):
    nets = []
    agents = []
    ge = []

    for genome_id, genome in genomes:
        genome.fitness = 0
        net = neat.nn.FeedForwardNetwork.create(genome, config)
        nets.append(net)
        ge.append(genome)

    run = True
    while run:
        data = get_unity_data()
        if(data == "new"):
            break
        agents = parse_unity_data(data)

        unity_outputs = []

        for x, agent in enumerate(agents):
            if not(agent.is_alive):
                unity_outputs.append([False, False])
                continue

            ge[x].fitness = agent.current_score

            output = nets[agents.index(agent)].activate([
                agent.sensors[0],
                agent.sensors[1],
                agent.sensors[2],
                agent.sensors[3],
                agent.sensors[4],
                agent.sensors[5],
                agent.sensors[6],
                agent.sensors[7],
                agent.position[0],
                agent.position[1],
                agent.health,
                agent.target_position[0],
                agent.target_position[1],
                agent.current_weapon[0],
                agent.current_weapon[1],
                agent.current_weapon[2]])

            unity_output = []
            for i in range(8):
                unity_output.append(False)

            max_value = 0
            max_value_idx = 0

            for i in range(len(output)):
                if output[i] > max_value:
                    max_value = output[i]
                    max_value_idx = i
            unity_output[max_value_idx] = True
            unity_outputs.append(unity_output.copy())
        all_queues[1].put(unity_outputs)


def run_population(population, eval_genomes_func, generations):
    population.run(eval_genomes_func, generations)


def waiting_for_commands():
    global config_hexagons, config_circles, config_squares
    global config_triangles, config_rectangles, config_player_agents
    global population_hexagons, population_circles, population_squares
    global population_triangles, population_rectangles, population_player_agents
    threads = []
    while True:
        data = client_socket.recv(4096 * 4).decode()

        parsed_json = json.loads(data)

        if parsed_json['command'] == 'Initialize algorithm':
            local_dir = os.path.dirname(__file__)
            config_hex_path = os.path.join(local_dir, 'config-hexagons.txt')
            config_cir_path = os.path.join(local_dir, 'config-circles.txt')
            config_sqr_path = os.path.join(local_dir, 'config-squares.txt')
            config_tri_path = os.path.join(local_dir, 'config-triangles.txt')
            config_rect_path = os.path.join(local_dir, 'config-rectangles.txt')
            config_pl_path = os.path.join(local_dir, 'config-players.txt')
            config_hexagons = initialize_config(config_hex_path)
            config_circles = initialize_config(config_cir_path)
            config_squares = initialize_config(config_sqr_path)
            config_triangles = initialize_config(config_tri_path)
            config_rectangles = initialize_config(config_rect_path)
            config_player_agents = initialize_config(config_pl_path)
            print("configs created")
            send_unity_outputs("out")
            continue
        elif parsed_json['command'] == 'Create hexagons':
            if config_hexagons is not None:
                if population_hexagons is None:
                    population_hexagons = create_population(config_hexagons)
                    print("population created")
                t = threading.Thread(target=run_population, args=(population_hexagons, eval_genomes_enemies, 1))
                threads.append(t)
                all_queues.append(queue.Queue())
                send_unity_outputs("out")
            else:
                print("Error: config is None")
                exit(1)
        elif parsed_json['command'] == 'Create circles':
            if config_circles is not None:
                if population_circles is None:
                    population_circles = create_population(config_circles)
                    print("population created")
                t = threading.Thread(target=run_population, args=(population_circles, eval_genomes_enemies, 1))
                threads.append(t)
                all_queues.append(queue.Queue())
                send_unity_outputs("out")
            else:
                print("Error: config is None")
                exit(1)
        elif parsed_json['command'] == 'Create squares':
            if config_squares is not None:
                if population_squares is None:
                    population_squares = create_population(config_squares)
                    print("population created")
                t = threading.Thread(target=run_population, args=(population_squares, eval_genomes_enemies, 1))
                threads.append(t)
                all_queues.append(queue.Queue())
                send_unity_outputs("out")
            else:
                print("Error: config is None")
                exit(1)
        elif parsed_json['command'] == 'Create triangles':
            if config_triangles is not None:
                if population_triangles is None:
                    population_triangles = create_population(config_triangles)
                    print("population created")
                t = threading.Thread(target=run_population, args=(population_triangles, eval_genomes_enemies, 1))
                threads.append(t)
                all_queues.append(queue.Queue())
                send_unity_outputs("out")
            else:
                print("Error: config is None")
                exit(1)
        elif parsed_json['command'] == 'Create rectangles':
            if config_rectangles is not None:
                if population_rectangles is None:
                    population_rectangles = create_population(config_rectangles)
                    print("population created")
                t = threading.Thread(target=run_population, args=(population_rectangles, eval_genomes_enemies, 1))
                threads.append(t)
                all_queues.append(queue.Queue())
                send_unity_outputs("out")
            else:
                print("Error: config is None")
                exit(1)
        elif parsed_json['command'] == 'Create players':
            if config_player_agents is not None:
                if population_player_agents is None:
                    population_player_agents = create_population(config_player_agents)
                    print("population created")
                t = threading.Thread(target=run_population, args=(population_player_agents, eval_genomes_player_agents, 1))
                threads.append(t)
                all_queues.append(queue.Queue())
                send_unity_outputs("out")
            else:
                print("Error: config is None")
                exit(1)
        elif parsed_json['command'] == 'Run Algorithm':
            for t in threads:
                t.start()
            combined_output = []
            while any(thread.is_alive() for thread in threads):
                for q in all_queues:
                    outputs = q.get(timeout=0.1)
                    combined_output.extend(outputs)
                send_unity_outputs(combined_output)
        else:
            print("Error: wrong command")
            exit(1)


all_queues = []

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

host = '127.0.0.1'
port = 12345

server_socket.bind((host, port))

server_socket.listen(1)
print(f"The server is running on {host}:{port}")

client_socket, addr = server_socket.accept()
print(f"Connection established with {addr}")

waiting_for_commands()

client_socket.close()
