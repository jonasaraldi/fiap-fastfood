# PostgreSQL no Amazon RDS

A escolha do PostgreSQL como banco de dados para o projeto foi baseada em diversos fatores importantes:

### Monolito e relacionamentos entre entidades 
Mesmo com a separação dos contextos delimitados a nível de database, ainda existem relacionamentos entre as entidades de domínio. Neste estágio em que o projeto é um monolito, faz sentido manter esses dados em um banco de dados relacional. Posteriormente, com a segregação dos contextos delimitados em outros microsserviços, a estratégia de banco de dados pode ser reavaliada, optando por uma base NoSQL com estratégias de replicação.

### Familiaridade com SQL
A familiaridade com SQL foi um fator importante para garantir um desenvolvimento mais ágil. Bases de dados NoSQL têm maneiras diferentes de acessar dados e alterar estruturas, o que requer uma curva de aprendizado adicional.

### Performance
O PostgreSQL é conhecido por ser um banco de dados altamente performático, especialmente quando são adotadas técnicas como aplicação de índices, tabelas temporárias, entre outras.

### Praticidade nos migrations
A utilização do Entity Framework Core com a estratégia Code First facilitou muito a promoção de mudanças estruturais no banco de dados, eliminando a necessidade de gerenciar scripts manualmente. Isso torna o processo de migração mais ágil e menos propenso a erros.

### Disponibilidade
Ao utilizar o Amazon RDS (Relational Database Service), baseado no PostgreSQL, podemos aproveitar os recursos de alta disponibilidade oferecidos pela AWS. O RDS gerencia automaticamente tarefas como backup, replicação e failover, garantindo assim uma infraestrutura robusta e confiável para o banco de dados. Essa disponibilidade é essencial para garantir que o banco de dados esteja sempre acessível e funcionando corretamente, mesmo em caso de falhas ou interrupções.

Ao considerar esses fatores, o PostgreSQL se destacou como a escolha mais adequada para atender às necessidades atuais do projeto, oferecendo um equilíbrio entre familiaridade, desempenho, praticidade e disponibilidade no desenvolvimento e manutenção do banco de dados.