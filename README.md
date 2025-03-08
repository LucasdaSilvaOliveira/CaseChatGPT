# **Case Técnico - Gestão de Pedidos (Order Management System)**  

## **Contexto**  
Você foi contratado para desenvolver um sistema de gestão de pedidos para uma pequena empresa de e-commerce. O sistema permitirá que os administradores cadastrem produtos, gerenciem pedidos e acompanhem o status das vendas.  

## **Requisitos Funcionais**  
### 1. **Autenticação e Autorização**  
- O sistema deve permitir login via **JWT**.  
- Apenas usuários administradores podem cadastrar produtos.  
- Qualquer usuário autenticado pode fazer pedidos.  

### 2. **Gestão de Produtos**  
- CRUD de produtos (Nome, Descrição, Preço, Estoque).  

### 3. **Gestão de Pedidos**  
- Criar pedido (relacionando com produtos e usuário).  
- Atualizar status do pedido (**Pendente, Processando, Enviado, Concluído**).  
- Listar pedidos do usuário autenticado.  
- Apenas administradores podem visualizar todos os pedidos.
