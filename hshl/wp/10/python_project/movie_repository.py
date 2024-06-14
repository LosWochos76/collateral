import io
import pandas as pd
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt

def get_movies(page=0):
    return df.iloc[page * PAGE_SIZE:(page + 1) * PAGE_SIZE].to_dict('records')

def get_last_page():
    return len(df) // PAGE_SIZE

def get_movie(movie_id):
    index = df[df['ID'] == int(movie_id)].index
    if index.any():
        return df.loc[index].to_dict('records')[0]
    return None

def delete(movie_id):
    global df
    index = df[df['ID'] == int(movie_id)].index
    if index.any():
        df = df[index]
        return True
    return False

def update(values):
    global df
    index = df[df['ID'] == int(values['ID'])].index
    if index.any():
        for key, value in values.items():
            df.loc[index, key] = pd.Series(value).astype(df[key].dtype).item()
        return True
    return False

def get_movies_by_year_as_png():
    by_years = df.groupby('Year')['Film'].count()
    fig, ax = plt.subplots(figsize=(10, 6))

    by_years.plot(kind='bar', ax=ax)
    ax.set_title('Anzahl der Filme pro Jahr')
    ax.set_xlabel('Jahr')
    ax.set_ylabel('Anzahl der Filme')

    img = io.BytesIO()
    fig.savefig(img, format='png')
    img.seek(0)
    return img

PAGE_SIZE = 10
df = pd.read_csv('data/movies.csv')
df = df.sort_values(by=['Film'])
df['ID'] = range(1, len(df) + 1)
